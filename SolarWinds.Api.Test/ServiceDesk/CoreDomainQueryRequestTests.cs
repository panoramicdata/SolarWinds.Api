using System.Net;
using System.Text.Json;
using AwesomeAssertions;
using Refit;
using SolarWinds.Api.ServiceDesk.Helpers;
using SolarWinds.Api.ServiceDesk.Interfaces;
using SolarWinds.Api.ServiceDesk.Models;
using Xunit;

namespace SolarWinds.Api.Test.ServiceDesk;

public class CoreDomainQueryRequestTests
{
	[Fact]
	public async Task Problems_GetAll_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = CreateClient(capture);
		var api = CreateApi<IProblems>(client);

		await api.GetAllAsync(new GetProblemsRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/problems.json");
		var query = ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	[Fact]
	public async Task Problems_GetById_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler("{}");
		using var client = CreateClient(capture);
		var api = CreateApi<IProblems>(client);

		await api.GetAsync(123, new GetProblemsRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/problems/123.json");
		var query = ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	[Fact]
	public async Task Changes_GetAll_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = CreateClient(capture);
		var api = CreateApi<IChanges>(client);

		await api.GetAllAsync(new GetChangesRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/changes.json");
		var query = ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	[Fact]
	public async Task Changes_GetById_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler("{}");
		using var client = CreateClient(capture);
		var api = CreateApi<IChanges>(client);

		await api.GetAsync(123, new GetChangesRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/changes/123.json");
		var query = ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	[Fact]
	public async Task Solutions_GetAll_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = CreateClient(capture);
		var api = CreateApi<ISolutions>(client);

		await api.GetAllAsync(new GetSolutionsRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/solutions.json");
		var query = ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	[Fact]
	public async Task Solutions_GetById_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler("{}");
		using var client = CreateClient(capture);
		var api = CreateApi<ISolutions>(client);

		await api.GetAsync(123, new GetSolutionsRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/solutions/123.json");
		var query = ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	private static HttpClient CreateClient(HttpMessageHandler handler) => new(handler)
	{
		BaseAddress = new Uri("https://api.samanage.com")
	};

	private static T CreateApi<T>(HttpClient client)
		where T : class
	{
		var settings = new RefitSettings(new SystemTextJsonContentSerializer(new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
		}))
		{
			UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
		};

		return RestService.For<T>(client, settings);
	}

	private static Dictionary<string, string> ParseQuery(Uri uri)
	{
		var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		var query = uri.Query;
		if (string.IsNullOrWhiteSpace(query))
		{
			return result;
		}

		foreach (var pair in query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries))
		{
			var pieces = pair.Split('=', 2);
			var key = Uri.UnescapeDataString(pieces[0]);
			var value = pieces.Length > 1 ? Uri.UnescapeDataString(pieces[1]) : string.Empty;
			result[key] = value;
		}

		return result;
	}

	private sealed class CaptureHandler(string responseBody = "[]") : HttpMessageHandler
	{
		public HttpRequestMessage? LastRequest { get; private set; }

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			LastRequest = request;
			var response = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent(responseBody)
			};

			return Task.FromResult(response);
		}
	}
}
