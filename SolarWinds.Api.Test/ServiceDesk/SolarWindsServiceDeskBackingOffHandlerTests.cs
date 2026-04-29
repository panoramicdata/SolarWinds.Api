using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using SolarWinds.Api.Http;
using Xunit;

namespace SolarWinds.Api.Test.ServiceDesk;

public class SolarWindsServiceDeskBackingOffHandlerTests
{
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