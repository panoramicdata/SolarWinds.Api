namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class IncidentEntityGeneralInfoRequestTests
{
	/// <summary>
	/// A captured entity_general_info response, used to check the request shape and that
	/// the states and actions deserialize.
	/// </summary>
	private const string ResponseJson = """

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

	/// <summary>
	/// Executes GetEntityGeneralInfo_UsesExpectedEndpointAndDeserializesStates.
	/// </summary>
	[Fact]
	public async Task GetEntityGeneralInfo_UsesExpectedEndpointAndDeserializesStates()
	{
		var capture = new CaptureHandler(ResponseJson);
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);

		var incidentsApi = ServiceDeskTestApi.CreateApi<IIncidents>(client);

		var result = await incidentsApi.GetEntityGeneralInfoAsync(1315435, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.Method.Should().Be(HttpMethod.Get);
		capture.LastRequest.RequestUri.Should().NotBeNull();
		capture.LastRequest.RequestUri!.AbsolutePath.Should().Be("/entity_general_info/1315435.json");

		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
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

}
