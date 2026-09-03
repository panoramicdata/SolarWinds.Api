namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class IncidentTypeRequestTests
{
	/// <summary>
	/// Executes GetTypesList_WithPortalQueryParameters_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task GetTypesList_WithPortalQueryParameters_UsesExpectedQueryParameters()
	{
		const string responseJson = """
		[
			{ "id": 1, "name": "Incident", "label": "Incident", "position": 1 }
		]
		""";

		var capture = new CaptureHandler(responseJson);
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);

		var api = ServiceDeskTestApi.CreateApi<IIncidentTypes>(client);

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
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["page"].Should().Be("1");
		query["model"].Should().Be("Incident");
		query["op_type"].Should().Be("update");
		query["per_page"].Should().Be("25");
		query["name"].Should().Be(string.Empty);
		query["is_portal_mode"].Should().Be("False");

		result.Should().HaveCount(1);
		result[0].Id.Should().Be(1);
	}

}
