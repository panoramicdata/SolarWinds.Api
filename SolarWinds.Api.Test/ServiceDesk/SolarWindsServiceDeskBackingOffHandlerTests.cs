using Microsoft.Extensions.Logging.Abstractions;
using SolarWinds.Api.Http;

namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Tests for the Service Desk retry handler.
/// </summary>
public class SolarWindsServiceDeskBackingOffHandlerTests
{
	private const string PostBody = "{\"incident\":{\"name\":\"test\"}}";

	/// <summary>
	/// A 429 is retried and the request body is replayed.
	/// </summary>
	[Fact]
	public async Task SendAsync_Retries429AndPreservesBody()
	{
		var (status, inner) = await SendAsync(
			HttpMethod.Post,
			"purchases",
			"{\"name\":\"test\"}",
			2,
			null,
			Respond(429, retryAfterZero: true),
			Respond(200));

		status.Should().Be(HttpStatusCode.OK);
		inner.RequestBodies.Should().BeEquivalentTo(["{\"name\":\"test\"}", "{\"name\":\"test\"}"]);
	}

	/// <summary>
	/// Exhausted rate-limit headers trigger a retry.
	/// </summary>
	[Fact]
	public async Task SendAsync_RetriesWhenRateLimitHeadersShowExhausted()
	{
		var resetEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
		var (status, inner) = await SendAsync(
			HttpMethod.Get,
			"purchases",
			null,
			2,
			0,
			Respond(400, exhaustedResetEpoch: resetEpoch),
			Respond(200));

		status.Should().Be(HttpStatusCode.OK);
		inner.CallCount.Should().Be(2);
	}

	/// <summary>
	/// A plain 400 is returned without retry.
	/// </summary>
	[Fact]
	public async Task SendAsync_DoesNotRetryPlainBadRequest()
	{
		var (status, inner) = await SendAsync(HttpMethod.Get, "purchases", null, 2, null, Respond(400));

		status.Should().Be(HttpStatusCode.BadRequest);
		inner.CallCount.Should().Be(1);
	}

	/// <summary>
	/// A successful create carrying exhausted headers must not be retried, or the POST would be duplicated (MS-24568).
	/// </summary>
	[Fact]
	public async Task SendAsync_DoesNotRetrySuccessfulPostWhenRateLimitHeadersShowExhausted()
	{
		var resetEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
		var (status, inner) = await SendAsync(
			HttpMethod.Post,
			"incidents",
			PostBody,
			5,
			0,
			Respond(201, exhaustedResetEpoch: resetEpoch));

		status.Should().Be(HttpStatusCode.Created);
		inner.CallCount.Should().Be(1);
	}

	/// <summary>
	/// A successful GET carrying exhausted headers is not retried either.
	/// </summary>
	[Fact]
	public async Task SendAsync_DoesNotRetrySuccessfulGetWhenRateLimitHeadersShowExhausted()
	{
		var (status, inner) = await SendAsync(
			HttpMethod.Get,
			"incidents",
			null,
			5,
			0,
			Respond(200, exhaustedResetEpoch: string.Empty));

		status.Should().Be(HttpStatusCode.OK);
		inner.CallCount.Should().Be(1);
	}

	/// <summary>
	/// Transient gateway failures are retried for idempotent methods.
	/// </summary>
	[Fact]
	public async Task SendAsync_RetriesGetOnServiceUnavailable()
	{
		var (status, inner) = await SendAsync(
			HttpMethod.Get,
			"incidents",
			null,
			2,
			null,
			Respond(503),
			Respond(200));

		status.Should().Be(HttpStatusCode.OK);
		inner.CallCount.Should().Be(2);
	}

	/// <summary>
	/// Transient gateway failures are not retried for POST, so the caller decides.
	/// </summary>
	[Theory]
	[InlineData(HttpStatusCode.BadGateway)]
	[InlineData(HttpStatusCode.ServiceUnavailable)]
	[InlineData(HttpStatusCode.GatewayTimeout)]
	public async Task SendAsync_DoesNotRetryPostOnTransientGatewayStatus(HttpStatusCode statusCode)
	{
		var (status, inner) = await SendAsync(HttpMethod.Post, "incidents", PostBody, 5, null, Respond((int)statusCode));

		status.Should().Be(statusCode);
		inner.CallCount.Should().Be(1);
	}

	/// <summary>
	/// A 429 was rejected unprocessed, so even a POST is retried.
	/// </summary>
	[Fact]
	public async Task SendAsync_RetriesPostOn429()
	{
		var (status, inner) = await SendAsync(
			HttpMethod.Post,
			"incidents",
			PostBody,
			2,
			0,
			Respond(429, retryAfterZero: true),
			Respond(201));

		status.Should().Be(HttpStatusCode.Created);
		inner.CallCount.Should().Be(2);
	}

	private static Func<HttpRequestMessage, HttpResponseMessage> Respond(int statusCode, bool retryAfterZero = false, string? exhaustedResetEpoch = null)
		=> _ =>
		{
			var response = new HttpResponseMessage((HttpStatusCode)statusCode) { Content = new StringContent("{}") };
			if (retryAfterZero)
			{
				response.Headers.RetryAfter = new System.Net.Http.Headers.RetryConditionHeaderValue(TimeSpan.Zero);
			}

			if (exhaustedResetEpoch is not null)
			{
				response.Headers.Add("X-Ratelimit-Remaining", "0");
				if (exhaustedResetEpoch.Length > 0)
				{
					response.Headers.Add("X-Ratelimit-Reset", exhaustedResetEpoch);
				}
			}

			return response;
		};

	private static async Task<(HttpStatusCode Status, SequenceHandler Inner)> SendAsync(
		HttpMethod method,
		string resource,
		string? body,
		int maxAttemptCount,
		int? maxBackOffDelaySeconds,
		params Func<HttpRequestMessage, HttpResponseMessage>[] responses)
	{
		var inner = new SequenceHandler([.. responses]);
		var options = new SolarWindsServiceDeskClientOptions
		{
			BaseUrl = "https://api.samanage.com",
			AccessToken = "token",
			Logger = NullLogger.Instance,
			MaxAttemptCount = maxAttemptCount,
		};

		if (maxBackOffDelaySeconds is int seconds)
		{
			options.MaxBackOffDelaySeconds = seconds;
		}

		using var handler = new SolarWindsServiceDeskBackingOffHandler(options) { InnerHandler = inner };
		using var invoker = new HttpMessageInvoker(handler, disposeHandler: false);
		using var request = new HttpRequestMessage(method, $"https://api.samanage.com/{resource}.json");
		if (body is not null)
		{
			request.Content = new StringContent(body);
		}

		using var response = await invoker.SendAsync(request, CancellationToken.None);
		return (response.StatusCode, inner);
	}

	private sealed class SequenceHandler(List<Func<HttpRequestMessage, HttpResponseMessage>> responses) : HttpMessageHandler
	{
		private readonly Queue<Func<HttpRequestMessage, HttpResponseMessage>> _responses = new(responses);

		public int CallCount { get; private set; }
		public List<string?> RequestBodies { get; } = [];

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			CallCount++;
			RequestBodies.Add(request.Content == null ? null : await request.Content.ReadAsStringAsync(cancellationToken));
			return _responses.Dequeue()(request);
		}
	}
}
