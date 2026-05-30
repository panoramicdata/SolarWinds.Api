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

public class IncidentEntityGeneralInfoRequestTests
{
	[Fact]
	public async Task GetEntityGeneralInfo_UsesExpectedEndpointAndDeserializesStates()
	{
		const string responseJson = """
		{
			"help": {
				"controller": "incidents",
				"action": "show",
				"item": "overview"
			},
			"currentUserId": 5456291,
			"descriptionsDisplayMode": 0,
			"canViewAudits": true,
			"topBarSecondaryHeader": "#1315435",
			"newCommentFirst": true,
			"defaultTab": null,
			"updatable": true,
			"updatableState": true,
			"abilityUpdatable": true,
			"states": [
				{
					"id": 135893,
					"key": "New",
					"title": "New",
					"color": 1,
					"selected": true,
					"archived": false
				},
				{
					"id": 663599,
					"key": "Pending_Assignment",
					"title": "Pending Assignment",
					"color": 6,
					"selected": false,
					"archived": false
				}
			],
			"actions": [
				{
					"label": "Merge",
					"type": "menu",
					"link": "#",
					"kind": "merge_incidents",
					"no_href": true
				}
			],
			"adHocChangeEnabled": true,
			"hasMasking": false,
			"objectTypeData": {
				"allowIncidentToResolve": true
			}
		}
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

		var result = await incidentsApi.GetEntityGeneralInfoAsync(1315435, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.Method.Should().Be(HttpMethod.Get);
		capture.LastRequest.RequestUri.Should().NotBeNull();
		capture.LastRequest.RequestUri!.AbsolutePath.Should().Be("/entity_general_info/1315435.json");

		var query = ParseQuery(capture.LastRequest.RequestUri);
		query["object_type"].Should().Be("incident");
		query["action_page_type"].Should().Be("show");
		query["is_portal_mode"].Should().Be("false");

		result.Should().NotBeNull();
		result.States.Should().HaveCount(2);
		result.States[0].Id.Should().Be(135893);
		result.States[0].Title.Should().Be("New");
		result.States[0].Selected.Should().BeTrue();
		result.States[1].Id.Should().Be(663599);
		result.Actions.Should().HaveCount(1);
		result.ObjectTypeData.Should().NotBeNull();
		result.ObjectTypeData!.AllowIncidentToResolve.Should().BeTrue();
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
