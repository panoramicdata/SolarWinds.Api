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

public class IncidentTypeRequestTests
{
	[Fact]
	public async Task GetTypesList_WithPortalQueryParameters_UsesExpectedQueryParameters()
	{
		const string responseJson = """
		[
			{ "id": 1, "name": "Incident", "label": "Incident", "position": 1 }
		]
		""";

		var capture = new CaptureHandler(responseJson);
		using var client = new HttpClient(capture)
		{
			BaseAddress = new Uri("https://api.samanage.com")
		};

		var api = RestService.For<IIncidentTypes>(client, new RefitSettings(
			new SystemTextJsonContentSerializer(new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
			}))
		{
			UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
		});

		var result = await api.GetTypesListAsync(new GetIncidentTypesRequest
		{
			Page = 1,
			Model = "Incident",
			OpType = "update",
			PerPage = 25,
			Name = string.Empty,
			IsPortalMode = false,
		}, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/incident_types/types_list.json");
		var query = ParseQuery(capture.LastRequest.RequestUri);
		query["page"].Should().Be("1");
		query["model"].Should().Be("Incident");
		query["op_type"].Should().Be("update");
		query["per_page"].Should().Be("25");
		query["name"].Should().Be(string.Empty);
		query["is_portal_mode"].Should().Be("False");

		result.Should().HaveCount(1);
		result[0].Id.Should().Be(1);
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
