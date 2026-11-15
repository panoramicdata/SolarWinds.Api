namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Tests the query string built for software requests.
/// </summary>
public class SoftwareQueryRequestTests
{
	/// <summary>
	/// Portal search parameters are sent as the expected query parameters.
	/// </summary>
	[Fact]
	public async Task GetAll_WithPortalSearchParameters_UsesExpectedQueryParameters()
	{
		const string columns = "software,version,vendor,category,installs,first_detected,tag,windows_system_component,last_update,hidden,disable_auto_merge,is_primary";

		var request = await ServiceDeskTestApi.CaptureAsync<ISoftwares>(api => api.GetAsync(new GetSoftwaresRequest
		{
			ReportId = 8992186,
			Applied = true,
			Category = ["[Not Set]"],
			Software = [0],
			Columns = columns,
		}, CancellationToken.None));

		ServiceDeskTestApi.AssertGet(
			request,
			"/softwares.json",
			("report_id", "8992186"),
			("applied", "True"),
			("category[]", "[Not Set]"),
			("software[]", "0"),
			("columns", columns));
	}
}
