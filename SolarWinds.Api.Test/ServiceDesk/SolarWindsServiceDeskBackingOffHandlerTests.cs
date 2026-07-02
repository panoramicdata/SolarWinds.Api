using Microsoft.Extensions.Logging.Abstractions;
using SolarWinds.Api.Http;

namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class SolarWindsServiceDeskBackingOffHandlerTests
{
	/// <summary>
	/// Executes SendAsync_Retries429AndPreservesBody.
	/// </summary>
	[Fact]
	public async Task SendAsync_Retries429AndPreservesBody()
	{
		var innerHandler = new SequenceHandler([
			_ =>
			{
				var response = new HttpResponseMessage((HttpStatusCode)429)
				{
					Content = new StringContent("{}")
				};
				response.Headers.RetryAfter = new System.Net.Http.Headers.RetryConditionHeaderValue(TimeSpan.Zero);
				return response;
			},
			_ => new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent("{}")
			}
		]);

		var handler = new SolarWindsServiceDeskBackingOffHandler(new SolarWindsServiceDeskClientOptions
		{
			BaseUrl = "https://api.samanage.com",
			AccessToken = "token",
			Logger = NullLogger.Instance,
			MaxAttemptCount = 2,
		})
		{
			InnerHandler = innerHandler,
		};

		using var invoker = new HttpMessageInvoker(handler);
		using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.samanage.com/purchases.json")
		{
			Content = new StringContent("{\"name\":\"test\"}")
		};

		using var response = await invoker.SendAsync(request, CancellationToken.None);

		response.StatusCode.Should().Be(HttpStatusCode.OK);
		innerHandler.RequestBodies.Should().BeEquivalentTo(["{\"name\":\"test\"}", "{\"name\":\"test\"}"]);
	}

	/// <summary>
	/// Executes SendAsync_RetriesWhenRateLimitHeadersShowExhausted.
	/// </summary>
	[Fact]
	public async Task SendAsync_RetriesWhenRateLimitHeadersShowExhausted()
	{
		var resetEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var innerHandler = new SequenceHandler([
			_ =>
			{
				var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
				{
					Content = new StringContent("{}")
				};
				response.Headers.Add("X-Ratelimit-Remaining", "0");
				response.Headers.Add("X-Ratelimit-Reset", resetEpoch.ToString());
				return response;
			},
			_ => new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent("{}")
			}
		]);

		var handler = new SolarWindsServiceDeskBackingOffHandler(new SolarWindsServiceDeskClientOptions
		{
			BaseUrl = "https://api.samanage.com",
			AccessToken = "token",
			Logger = NullLogger.Instance,
			MaxAttemptCount = 2,
			MaxBackOffDelaySeconds = 0,
		})
		{
			InnerHandler = innerHandler,
		};

		using var invoker = new HttpMessageInvoker(handler);
		using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.samanage.com/purchases.json");

		using var response = await invoker.SendAsync(request, CancellationToken.None);

		response.StatusCode.Should().Be(HttpStatusCode.OK);
		innerHandler.CallCount.Should().Be(2);
	}

	/// <summary>
	/// Executes SendAsync_DoesNotRetryPlainBadRequest.
	/// </summary>
	[Fact]
	public async Task SendAsync_DoesNotRetryPlainBadRequest()
	{
		var innerHandler = new SequenceHandler([
			_ => new HttpResponseMessage(HttpStatusCode.BadRequest)
			{
				Content = new StringContent("{}")
			}
		]);

		var handler = new SolarWindsServiceDeskBackingOffHandler(new SolarWindsServiceDeskClientOptions
		{
			BaseUrl = "https://api.samanage.com",
			AccessToken = "token",
			Logger = NullLogger.Instance,
			MaxAttemptCount = 2,
		})
		{
			InnerHandler = innerHandler,
		};

		using var invoker = new HttpMessageInvoker(handler);
		using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.samanage.com/purchases.json");

		using var response = await invoker.SendAsync(request, CancellationToken.None);

		response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
		innerHandler.CallCount.Should().Be(1);
	}

	/// <summary>
	/// A successful create (201) carrying exhausted rate-limit headers must NOT be retried:
	/// the incident already exists, and re-sending the POST would create duplicates (MS-24568).
	/// </summary>
	[Fact]
	public async Task SendAsync_DoesNotRetrySuccessfulPostWhenRateLimitHeadersShowExhausted()
	{
		var resetEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var innerHandler = new SequenceHandler([
			_ =>
			{
				var response = new HttpResponseMessage(HttpStatusCode.Created)
				{
					Content = new StringContent("{\"id\":1}")
				};
				response.Headers.Add("X-Ratelimit-Remaining", "0");
				response.Headers.Add("X-Ratelimit-Reset", resetEpoch.ToString());
				return response;
			}
		]);

		var handler = new SolarWindsServiceDeskBackingOffHandler(new SolarWindsServiceDeskClientOptions
		{
			BaseUrl = "https://api.samanage.com",
			AccessToken = "token",
			Logger = NullLogger.Instance,
			MaxAttemptCount = 5,
			MaxBackOffDelaySeconds = 0,
		})
		{
			InnerHandler = innerHandler,
		};

		using var invoker = new HttpMessageInvoker(handler);
		using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.samanage.com/incidents.json")
		{
			Content = new StringContent("{\"incident\":{\"name\":\"test\"}}")
		};

		using var response = await invoker.SendAsync(request, CancellationToken.None);

		response.StatusCode.Should().Be(HttpStatusCode.Created);
		innerHandler.CallCount.Should().Be(1);
	}

	/// <summary>
	/// A successful GET carrying exhausted rate-limit headers must NOT be retried either;
	/// the result is already in hand and re-sending only burns more quota.
	/// </summary>
	[Fact]
	public async Task SendAsync_DoesNotRetrySuccessfulGetWhenRateLimitHeadersShowExhausted()
	{
		var innerHandler = new SequenceHandler([
			_ =>
			{
				var response = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent("[]")
				};
				response.Headers.Add("X-Ratelimit-Remaining", "0");
				return response;
			}
		]);

		var handler = new SolarWindsServiceDeskBackingOffHandler(new SolarWindsServiceDeskClientOptions
		{
			BaseUrl = "https://api.samanage.com",
			AccessToken = "token",
			Logger = NullLogger.Instance,
			MaxAttemptCount = 5,
			MaxBackOffDelaySeconds = 0,
		})
		{
			InnerHandler = innerHandler,
		};

		using var invoker = new HttpMessageInvoker(handler);
		using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.samanage.com/incidents.json");

		using var response = await invoker.SendAsync(request, CancellationToken.None);

		response.StatusCode.Should().Be(HttpStatusCode.OK);
		innerHandler.CallCount.Should().Be(1);
	}

	/// <summary>
	/// Transient gateway failures are retried for idempotent methods (GET).
	/// </summary>
	[Fact]
	public async Task SendAsync_RetriesGetOnServiceUnavailable()
	{
		var innerHandler = new SequenceHandler([
			_ => new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
			{
				Content = new StringContent("{}")
			},
			_ => new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent("{}")
			}
		]);

		var handler = new SolarWindsServiceDeskBackingOffHandler(new SolarWindsServiceDeskClientOptions
		{
			BaseUrl = "https://api.samanage.com",
			AccessToken = "token",
			Logger = NullLogger.Instance,
			MaxAttemptCount = 2,
		})
		{
			InnerHandler = innerHandler,
		};

		using var invoker = new HttpMessageInvoker(handler);
		using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.samanage.com/incidents.json");

		using var response = await invoker.SendAsync(request, CancellationToken.None);

		response.StatusCode.Should().Be(HttpStatusCode.OK);
		innerHandler.CallCount.Should().Be(2);
	}

	/// <summary>
	/// Transient gateway failures are NOT retried for non-idempotent methods (POST): a 502/504
	/// does not guarantee the origin failed to process the request, so re-sending risks duplicates.
	/// The response is returned so the caller can decide.
	/// </summary>
	[Theory]
	[InlineData(HttpStatusCode.BadGateway)]
	[InlineData(HttpStatusCode.ServiceUnavailable)]
	[InlineData(HttpStatusCode.GatewayTimeout)]
	public async Task SendAsync_DoesNotRetryPostOnTransientGatewayStatus(HttpStatusCode statusCode)
	{
		var innerHandler = new SequenceHandler([
			_ => new HttpResponseMessage(statusCode)
			{
				Content = new StringContent("{}")
			}
		]);

		var handler = new SolarWindsServiceDeskBackingOffHandler(new SolarWindsServiceDeskClientOptions
		{
			BaseUrl = "https://api.samanage.com",
			AccessToken = "token",
			Logger = NullLogger.Instance,
			MaxAttemptCount = 5,
		})
		{
			InnerHandler = innerHandler,
		};

		using var invoker = new HttpMessageInvoker(handler);
		using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.samanage.com/incidents.json")
		{
			Content = new StringContent("{\"incident\":{\"name\":\"test\"}}")
		};

		using var response = await invoker.SendAsync(request, CancellationToken.None);

		response.StatusCode.Should().Be(statusCode);
		innerHandler.CallCount.Should().Be(1);
	}

	/// <summary>
	/// A 429 means the request was rejected without being processed, so even a POST is safe to retry.
	/// (Covers the same path as SendAsync_Retries429AndPreservesBody; kept explicit for the method-safety rule.)
	/// </summary>
	[Fact]
	public async Task SendAsync_RetriesPostOn429()
	{
		var innerHandler = new SequenceHandler([
			_ =>
			{
				var response = new HttpResponseMessage((HttpStatusCode)429)
				{
					Content = new StringContent("{}")
				};
				response.Headers.RetryAfter = new System.Net.Http.Headers.RetryConditionHeaderValue(TimeSpan.Zero);
				return response;
			},
			_ => new HttpResponseMessage(HttpStatusCode.Created)
			{
				Content = new StringContent("{\"id\":1}")
			}
		]);

		var handler = new SolarWindsServiceDeskBackingOffHandler(new SolarWindsServiceDeskClientOptions
		{
			BaseUrl = "https://api.samanage.com",
			AccessToken = "token",
			Logger = NullLogger.Instance,
			MaxAttemptCount = 2,
			MaxBackOffDelaySeconds = 0,
		})
		{
			InnerHandler = innerHandler,
		};

		using var invoker = new HttpMessageInvoker(handler);
		using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.samanage.com/incidents.json")
		{
			Content = new StringContent("{\"incident\":{\"name\":\"test\"}}")
		};

		using var response = await invoker.SendAsync(request, CancellationToken.None);

		response.StatusCode.Should().Be(HttpStatusCode.Created);
		innerHandler.CallCount.Should().Be(2);
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
