namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class IncidentAugmentedTitlesRequestTests
{
	/// <summary>
	/// Executes GetAugmentedTitles_UsesExpectedQueryParameters_AndDeserializesResponse.
	/// </summary>
	[Fact]
	public async Task GetAugmentedTitles_UsesExpectedQueryParameters_AndDeserializesResponse()
	{
		const string responseJson = """
		[
			{ "id": 182722302, "title": "#182722302 Printer ticket" }
		]
		""";

		var capture = new CaptureHandler(responseJson);
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);

		var incidentsApi = ServiceDeskTestApi.CreateApi<IIncidents>(client);

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

		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
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

}
