using System.Net;
using System.Text;
using System.Text.Json;
using AwesomeAssertions;
using Refit;
using SolarWinds.Api.ServiceDesk.Helpers;
using SolarWinds.Api.ServiceDesk.Interfaces;
using SolarWinds.Api.ServiceDesk.Models;
using Xunit;

namespace SolarWinds.Api.Test.ServiceDesk;

public class IncidentAugmentedTitlesRequestTests
{
	[Fact]
	public async Task GetAugmentedTitles_UsesExpectedQueryParameters_AndDeserializesResponse()
	{
		const string responseJson = """
		[
			{ "id": 182722302, "title": "#182722302 Printer ticket" }
		]
		""";

		var capture = new CaptureHandler(responseJson);
		using var client = new HttpClient(capture)
		{
			BaseAddress = new Uri("https://api.samanage.com")
		};

		var incidentsApi = RestService.For<IIncidents>(client, new RefitSettings(
			new SystemTextJsonContentSerializer(new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
			}))
		{
			UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
		});

		var result = await incidentsApi.GetAugmentedTitlesAsync(new GetAugmentedTitlesRequest
		{
			Ids = [182722302],
			Model = "incident",
			Unmasked = false,
			IsPortalMode = false,
		}, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.Method.Should().Be(HttpMethod.Get);
		capture.LastRequest.RequestUri.Should().NotBeNull();
		capture.LastRequest.RequestUri!.AbsolutePath.Should().Be("/augmented_titles.json");

		var query = ParseQuery(capture.LastRequest.RequestUri);
		query.Should().ContainKey("ids[]");
		query["ids[]"].Should().Be("182722302");
		query.Should().ContainKey("model");
		query["model"].Should().Be("incident");
		query.Should().ContainKey("unmasked");
		query["unmasked"].Should().Be("False");
		query.Should().ContainKey("is_portal_mode");
		query["is_portal_mode"].Should().Be("False");

		result.Should().HaveCount(1);
		result[0].Id.Should().Be(182722302);
		result[0].Title.Should().Be("#182722302 Printer ticket");
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

	private sealed class CaptureHandler(string responseContent) : HttpMessageHandler
	{
		public HttpRequestMessage? LastRequest { get; private set; }

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			LastRequest = request;
			var response = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
			};

			return Task.FromResult(response);
		}
	}
}
