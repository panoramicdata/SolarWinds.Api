namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class SoftwareQueryRequestTests
{
	/// <summary>
	/// Executes GetAll_WithPortalSearchParameters_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task GetAll_WithPortalSearchParameters_UsesExpectedQueryParameters()
	{
		const string columns = "software,version,vendor,category,installs,first_detected,tag,windows_system_component,last_update,hidden,disable_auto_merge,is_primary";

		var capture = new CaptureHandler();
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);

		var softwaresApi = ServiceDeskTestApi.CreateApi<ISoftwares>(client);

		await softwaresApi.GetAsync(new GetSoftwaresRequest
		{
			ReportId = 8992186,
			Applied = true,
			Category = ["[Not Set]"],
			Software = [0],
			Columns = columns,
		}, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.Method.Should().Be(HttpMethod.Get);
		capture.LastRequest.RequestUri.Should().NotBeNull();
		capture.LastRequest.RequestUri!.AbsolutePath.Should().Be("/softwares.json");

		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query.Should().ContainKey("report_id");
		query["report_id"].Should().Be("8992186");
		query.Should().ContainKey("applied");
		query["applied"].Should().Be("True");
		query.Should().ContainKey("category[]");
		query["category[]"].Should().Be("[Not Set]");
		query.Should().ContainKey("software[]");
		query["software[]"].Should().Be("0");
		query.Should().ContainKey("columns");
		query["columns"].Should().Be(columns);
	}

}
