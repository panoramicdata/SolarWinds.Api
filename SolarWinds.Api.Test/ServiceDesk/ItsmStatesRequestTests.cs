namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class ItsmStatesRequestTests
{
	/// <summary>
	/// Executes InitTabsData_WithPortalSettings_UsesExpectedQueryParameters_AndDeserializes.
	/// </summary>
	[Fact]
	public async Task InitTabsData_WithPortalSettings_UsesExpectedQueryParameters_AndDeserializes()
	{
		const string responseJson = """
		{
			"tabs": [
				{ "key": "custom_states_incident", "title": "Incident", "visible": true }
			],
			"available": true
		}
		""";

		var capture = new CaptureHandler(responseJson);
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);

		var api = ServiceDeskTestApi.CreateApi<ISetupItsmStates>(client);

		var result = await api.InitTabsDataAsync(new GetItsmStatesInitTabsDataRequest
		{
			CustomStatesIncidentVisibilityKey = "feature_acquired",
			CustomStatesIncidentVisibilityValue = "custom_incident_lifecycle",
			CustomStatesChangeVisibilityKey = "feature_acquired",
			CustomStatesChangeVisibilityValue = "custom_incident_lifecycle",
			CustomStatesProjectVisibilityValue = "Hiddables::EnableProjectManagementFeature",
			CustomStatesAssetVisibilityKey = "feature_enabled",
			CustomStatesAssetVisibilityValue = "Hiddables::AssetsCustomStates",
			IsPortalMode = false,
		}, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/setup/itsm_states/init_tabs_data.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["settings[custom_states_incident][visibilityKey]"].Should().Be("feature_acquired");
		query["settings[custom_states_incident][visibilityValue]"].Should().Be("custom_incident_lifecycle");
		query["settings[custom_states_change][visibilityKey]"].Should().Be("feature_acquired");
		query["settings[custom_states_change][visibilityValue]"].Should().Be("custom_incident_lifecycle");
		query["settings[custom_states_project][visibilityValue]"].Should().Be("Hiddables::EnableProjectManagementFeature");
		query["settings[custom_states_asset][visibilityKey]"].Should().Be("feature_enabled");
		query["settings[custom_states_asset][visibilityValue]"].Should().Be("Hiddables::AssetsCustomStates");
		query["is_portal_mode"].Should().Be("False");

		result.Tabs.Should().ContainSingle();
		result.Tabs[0].Key.Should().Be("custom_states_incident");
	}

	/// <summary>
	/// Executes InitStatesData_WithIncidentType_UsesExpectedQueryParameters_AndDeserializes.
	/// </summary>
	[Fact]
	public async Task InitStatesData_WithIncidentType_UsesExpectedQueryParameters_AndDeserializes()
	{
		const string responseJson = """
		{
			"states": [
				{ "id": 135893, "key": "New", "title": "New", "color": 1, "archived": false },
				{ "id": 663599, "key": "Resolved", "title": "Resolved", "color": 2, "archived": false }
			]
		}
		""";

		var capture = new CaptureHandler(responseJson);
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);

		var api = ServiceDeskTestApi.CreateApi<ISetupItsmStates>(client);

		var result = await api.InitStatesDataAsync(new GetItsmStatesInitStatesDataRequest
		{
			ItsmType = "Incident",
			IsPortalMode = false,
		}, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/setup/itsm_states/init_states_data.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["itsm_type"].Should().Be("Incident");
		query["is_portal_mode"].Should().Be("False");

		result.States.Should().HaveCount(2);
		result.States[1].Key.Should().Be("Resolved");
	}

}
