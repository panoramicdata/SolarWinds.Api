using System.Net;
using Microsoft.Extensions.Logging;

namespace SolarWinds.Api.Http;

/// <summary>
/// Retries Service Desk requests when the API signals rate limiting or transient gateway availability issues.
/// </summary>
public partial class SolarWindsServiceDeskBackingOffHandler(SolarWindsServiceDeskClientOptions options) : DelegatingHandler
{
	private readonly SolarWindsServiceDeskClientOptions _options = options;

	/// <summary>
	/// Sends an HTTP request and retries according to Service Desk rate-limit and transient-failure signals.
	/// </summary>
	/// <param name="request">Request to send.</param>
	/// <param name="cancellationToken">Token used to cancel the request pipeline.</param>
	/// <returns>The final response after retries, or the first non-retriable response.</returns>
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		EnsureRequestAllowed(request);

		var bufferedRequest = await BufferedRequest.CreateAsync(request, cancellationToken).ConfigureAwait(false);

		// The attempt limit is enforced inside the loop, because deciding whether to retry needs the
		// response that has not been received yet at the top of an iteration.
		var attempt = 0;
		while (true)
		{
			attempt++;

			using var requestToSend = bufferedRequest.CreateRequestMessage();
			var response = await base.SendAsync(requestToSend, cancellationToken).ConfigureAwait(false);

			if (!TryGetRetryDelay(request.Method, response, attempt, out var delay, out var reason) || attempt >= _options.MaxAttemptCount)
			{
				return response;
			}

			_options.Logger?.LogInformation(
				"Retrying {Method} {Uri} after attempt {Attempt}/{MaxAttempts}. Waiting {DelaySeconds:N2}s because {Reason}.",
				request.Method,
				request.RequestUri,
				attempt,
				_options.MaxAttemptCount,
				delay.TotalSeconds,
				reason);

			response.Dispose();
			await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
		}
	}

	private void EnsureRequestAllowed(HttpRequestMessage request)
	{
		if (_options.IsReadOnly && !IsReadOnlyMethod(request.Method))
		{
			throw new InvalidOperationException($"The client is read-only; {request.Method} {request.RequestUri?.AbsolutePath} would change the server.");
		}
	}

	private static bool IsReadOnlyMethod(HttpMethod method)
		=> method == HttpMethod.Get
			|| method == HttpMethod.Head
			|| method == HttpMethod.Options;

	/// <summary>
	/// Determines whether a response should be retried and computes the delay before retrying.
	/// A response that indicates success is never retried: the request has already been processed,
	/// and re-sending a non-idempotent request (e.g. an incident-creating POST) would duplicate it,
	/// even when rate-limit headers show the quota is exhausted.
	/// Transient gateway failures are only retried for idempotent methods, because a 502/504 does
	/// not guarantee the origin did not process the request.
	/// </summary>
	/// <param name="method">HTTP method of the request that produced the response.</param>
	/// <param name="response">Response received from the server.</param>
	/// <param name="attempt">Current attempt number (1-based).</param>
	/// <param name="delay">Computed delay before the next attempt.</param>
	/// <param name="reason">Human-readable reason for the retry decision.</param>
	/// <returns><see langword="true"/> when the request should be retried; otherwise <see langword="false"/>.</returns>
	internal bool TryGetRetryDelay(HttpMethod method, HttpResponseMessage response, int attempt, out TimeSpan delay, out string reason)
	{
		if (response.IsSuccessStatusCode)
		{
			delay = TimeSpan.Zero;
			reason = string.Empty;
			return false;
		}

		// A rate-limited request was rejected without being processed, so it is safe to re-send for any method.
		if (TryGetRateLimitDelay(response, attempt, out delay, out reason))
		{
			return true;
		}

		switch (response.StatusCode)
		{
			case HttpStatusCode.BadGateway:
			case HttpStatusCode.ServiceUnavailable:
			case HttpStatusCode.GatewayTimeout:
				if (!IsIdempotent(method))
				{
					delay = TimeSpan.Zero;
					reason = string.Empty;
					return false;
				}

				delay = TimeSpan.FromSeconds(5);
				reason = $"transient status {(int)response.StatusCode}";
				return true;
			default:
				delay = TimeSpan.Zero;
				reason = string.Empty;
				return false;
		}
	}

	/// <summary>
	/// Determines whether an HTTP method is idempotent and therefore safe to re-send when the
	/// origin may already have processed the request.
	/// </summary>
	/// <param name="method">The HTTP method to classify.</param>
	/// <returns><see langword="true"/> for idempotent methods; otherwise <see langword="false"/>.</returns>
	internal static bool IsIdempotent(HttpMethod method)
		=> method == HttpMethod.Get
			|| method == HttpMethod.Head
			|| method == HttpMethod.Options
			|| method == HttpMethod.Trace
			|| method == HttpMethod.Put
			|| method == HttpMethod.Delete;

	private bool TryGetRateLimitDelay(HttpResponseMessage response, int attempt, out TimeSpan delay, out string reason)
	{
		var hasResetHeader = ServiceDeskRateLimitHeaders.TryGetResetDelay(response, out var resetDelay);
		var isRateLimitStatus = response.StatusCode == (HttpStatusCode)429;
		var hasRemainingZero = ServiceDeskRateLimitHeaders.TryGetHeaderInt(response, "X-Ratelimit-Remaining", out var remaining) && remaining <= 0;

		if (!isRateLimitStatus && !hasRemainingZero)
		{
			delay = TimeSpan.Zero;
			reason = string.Empty;
			return false;
		}

		var retryAfterDelay = ServiceDeskRateLimitHeaders.GetRetryAfterDelay(response);
		var fallbackDelay = CalculateBackoffDelay(attempt, _options.BackOffDelayFactor, _options.MaxBackOffDelaySeconds);
		var selectedDelay = ServiceDeskRateLimitHeaders.Max(retryAfterDelay, resetDelay, fallbackDelay);

		if (selectedDelay < TimeSpan.Zero)
		{
			selectedDelay = TimeSpan.Zero;
		}

		delay = selectedDelay;
		reason = isRateLimitStatus
			? "HTTP 429"
			: hasResetHeader
				? "X-Ratelimit-Remaining reached zero"
				: "rate limiting";
		return true;
	}

	/// <summary>
	/// Calculates exponential backoff delay for a retry attempt.
	/// </summary>
	/// <param name="attempt">Current retry attempt number (1-based).</param>
	/// <param name="backOffDelayFactor">Exponential multiplier applied per attempt.</param>
	/// <param name="maxBackOffDelaySeconds">Maximum delay cap in seconds.</param>
	/// <returns>Bounded backoff delay for the attempt.</returns>
	internal static TimeSpan CalculateBackoffDelay(int attempt, double backOffDelayFactor, int maxBackOffDelaySeconds)
	{
		var seconds = Math.Pow(backOffDelayFactor, Math.Max(attempt - 1, 0));
		return TimeSpan.FromSeconds(Math.Min(seconds, maxBackOffDelaySeconds));
	}
}
