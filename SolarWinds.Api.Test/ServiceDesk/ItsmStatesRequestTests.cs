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
		using var client = new HttpClient(capture)
		{
			BaseAddress = new Uri("https://api.samanage.com")
		};

		var api = RestService.For<ISetupItsmStates>(client, new RefitSettings(
			new SystemTextJsonContentSerializer(new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
			}))
		{
			UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
		});

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
		var query = ParseQuery(capture.LastRequest.RequestUri);
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
		using var client = new HttpClient(capture)
		{
			BaseAddress = new Uri("https://api.samanage.com")
		};

		var api = RestService.For<ISetupItsmStates>(client, new RefitSettings(
			new SystemTextJsonContentSerializer(new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
			}))
		{
			UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
		});

		var result = await api.InitStatesDataAsync(new GetItsmStatesInitStatesDataRequest
		{
			ItsmType = "Incident",
			IsPortalMode = false,
		}, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/setup/itsm_states/init_states_data.json");
		var query = ParseQuery(capture.LastRequest.RequestUri);
		query["itsm_type"].Should().Be("Incident");
		query["is_portal_mode"].Should().Be("False");

		result.States.Should().HaveCount(2);
		result.States[1].Key.Should().Be("Resolved");
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
