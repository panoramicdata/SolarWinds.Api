using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SolarWinds.Api.Http;

/// <summary>
/// Retries Service Desk requests when the API signals rate limiting or transient gateway availability issues.
/// </summary>
public class SolarWindsServiceDeskBackingOffHandler(SolarWindsServiceDeskClientOptions options) : DelegatingHandler
{
	private readonly SolarWindsServiceDeskClientOptions _options = options;

	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		if (request == null)
		{
			throw new ArgumentNullException(nameof(request));
		}

		var bufferedRequest = await BufferedRequest.CreateAsync(request, cancellationToken).ConfigureAwait(false);

		for (var attempt = 1; ; attempt++)
		{
			using var requestToSend = bufferedRequest.CreateRequestMessage();
			var response = await base.SendAsync(requestToSend, cancellationToken).ConfigureAwait(false);

			if (!TryGetRetryDelay(response, attempt, out var delay, out var reason) || attempt >= _options.MaxAttemptCount)
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

	internal bool TryGetRetryDelay(HttpResponseMessage response, int attempt, out TimeSpan delay, out string reason)
	{
		if (TryGetRateLimitDelay(response, attempt, out delay, out reason))
		{
			return true;
		}

		switch (response.StatusCode)
		{
			case HttpStatusCode.BadGateway:
			case HttpStatusCode.ServiceUnavailable:
			case HttpStatusCode.GatewayTimeout:
				delay = TimeSpan.FromSeconds(5);
				reason = $"transient status {(int)response.StatusCode}";
				return true;
			default:
				delay = TimeSpan.Zero;
				reason = string.Empty;
				return false;
		}
	}

	private bool TryGetRateLimitDelay(HttpResponseMessage response, int attempt, out TimeSpan delay, out string reason)
	{
		var hasResetHeader = TryGetResetDelay(response, out var resetDelay);
		var isRateLimitStatus = response.StatusCode == (HttpStatusCode)429;
		var hasRemainingZero = TryGetHeaderInt(response, "X-Ratelimit-Remaining", out var remaining) && remaining <= 0;

		if (!isRateLimitStatus && !hasRemainingZero)
		{
			delay = TimeSpan.Zero;
			reason = string.Empty;
			return false;
		}

		var retryAfterDelay = GetRetryAfterDelay(response);
		var fallbackDelay = CalculateBackoffDelay(attempt, _options.BackOffDelayFactor, _options.MaxBackOffDelaySeconds);
		var selectedDelay = Max(retryAfterDelay, resetDelay, fallbackDelay);

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

	internal static TimeSpan CalculateBackoffDelay(int attempt, double backOffDelayFactor, int maxBackOffDelaySeconds)
	{
		var seconds = Math.Pow(backOffDelayFactor, Math.Max(attempt - 1, 0));
		return TimeSpan.FromSeconds(Math.Min(seconds, maxBackOffDelaySeconds));
	}

	private static TimeSpan GetRetryAfterDelay(HttpResponseMessage response)
	{
		var retryAfter = response.Headers.RetryAfter;
		if (retryAfter?.Delta is TimeSpan delta)
		{
			return delta < TimeSpan.Zero ? TimeSpan.Zero : delta;
		}

		if (retryAfter?.Date is DateTimeOffset date)
		{
			var delay = date - DateTimeOffset.UtcNow;
			return delay < TimeSpan.Zero ? TimeSpan.Zero : delay;
		}

		return TimeSpan.Zero;
	}

	private static bool TryGetResetDelay(HttpResponseMessage response, out TimeSpan delay)
	{
		if (!TryGetHeaderLong(response, "X-Ratelimit-Reset", out var resetEpoch))
		{
			delay = TimeSpan.Zero;
			return false;
		}

		var nowEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		delay = TimeSpan.FromSeconds(Math.Max(resetEpoch - nowEpoch, 0));
		return true;
	}

	private static bool TryGetHeaderInt(HttpResponseMessage response, string headerName, out int value)
		=> TryGetHeader(response, headerName, int.TryParse, out value);

	private static bool TryGetHeaderLong(HttpResponseMessage response, string headerName, out long value)
		=> TryGetHeader(response, headerName, long.TryParse, out value);

	private static bool TryGetHeader<T>(HttpResponseMessage response, string headerName, TryParseHandler<T> tryParse, out T value)
	{
		if (TryGetHeaderValues(response, headerName, out var headerValue) && tryParse(headerValue, out value))
		{
			return true;
		}

		value = default!;
		return false;
	}

	private static bool TryGetHeaderValues(HttpResponseMessage response, string headerName, out string value)
	{
		if (response.Headers.TryGetValues(headerName, out var headerValues))
		{
			value = headerValues.FirstOrDefault() ?? string.Empty;
			return !string.IsNullOrWhiteSpace(value);
		}

		if (response.Content.Headers.TryGetValues(headerName, out headerValues))
		{
			value = headerValues.FirstOrDefault() ?? string.Empty;
			return !string.IsNullOrWhiteSpace(value);
		}

		value = string.Empty;
		return false;
	}

	private static TimeSpan Max(params TimeSpan[] values) => values.Max();

	private delegate bool TryParseHandler<T>(string input, out T value);

	private sealed class BufferedRequest
	{
		private BufferedRequest(HttpMethod method, Uri? requestUri, Version version, HttpVersionPolicy versionPolicy)
		{
			Method = method;
			RequestUri = requestUri;
			Version = version;
			VersionPolicy = versionPolicy;
		}

		public HttpMethod Method { get; }
		public Uri? RequestUri { get; }
		public Version Version { get; }
		public HttpVersionPolicy VersionPolicy { get; }
		public byte[]? ContentBytes { get; private set; }
		public string? ContentMediaType { get; private set; }
		public HeaderBag RequestHeaders { get; } = [];
		public HeaderBag ContentHeaders { get; } = [];
		public OptionBag Options { get; } = [];

		public static async Task<BufferedRequest> CreateAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var bufferedRequest = new BufferedRequest(request.Method, request.RequestUri, request.Version, request.VersionPolicy);

			foreach (var header in request.Headers)
			{
				bufferedRequest.RequestHeaders.Add((header.Key, [.. header.Value]));
			}

			foreach (var option in request.Options)
			{
				bufferedRequest.Options.Add((option.Key, option.Value));
			}

			if (request.Content != null)
			{
				bufferedRequest.ContentBytes = await request.Content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
				bufferedRequest.ContentMediaType = request.Content.Headers.ContentType?.MediaType;

				foreach (var header in request.Content.Headers)
				{
					bufferedRequest.ContentHeaders.Add((header.Key, [.. header.Value]));
				}
			}

			return bufferedRequest;
		}

		public HttpRequestMessage CreateRequestMessage()
		{
			var request = new HttpRequestMessage(Method, RequestUri)
			{
				Version = Version,
				VersionPolicy = VersionPolicy,
			};

			foreach (var header in RequestHeaders)
			{
				request.Headers.TryAddWithoutValidation(header.Name, header.Values);
			}

			foreach (var option in Options)
			{
				request.Options.Set(new HttpRequestOptionsKey<object?>(option.Key), option.Value);
			}

			if (ContentBytes != null)
			{
				request.Content = new ByteArrayContent(ContentBytes);

				foreach (var header in ContentHeaders)
				{
					request.Content.Headers.TryAddWithoutValidation(header.Name, header.Values);
				}
			}

			return request;
		}
	}

	private sealed class HeaderBag : System.Collections.Generic.List<(string Name, string[] Values)>;
	private sealed class OptionBag : System.Collections.Generic.List<(string Key, object? Value)>;
}