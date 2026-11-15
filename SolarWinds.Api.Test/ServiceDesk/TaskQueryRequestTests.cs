namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Tests the query string built for task requests.
/// </summary>
public class TaskQueryRequestTests
{
	/// <summary>
	/// Portal search parameters are sent as the expected query parameters.
	/// </summary>
	[Fact]
	public async Task GetAll_WithPortalSearchParameters_UsesExpectedQueryParameters()
	{
		const string columns = "type,title,assigned_to,requester,source,due_date";
		int[] assignedTo = [16453692, 16453698, 16453699, 16453703, 16453704, 16453705, 16453706, 16453707, 16453731];

		var request = await ServiceDeskTestApi.CaptureAsync<ITasks>(api => api.GetAsync(new GetTasksRequest
		{
			ReportId = 8992259,
			Applied = true,
			AssignedTo = assignedTo,
			Columns = columns,
		}, CancellationToken.None));

		ServiceDeskTestApi.AssertGet(request, "/tasks.json", ("report_id", "8992259"), ("applied", "True"), ("columns", columns));
		ServiceDeskTestApi.AssertRepeated(request, "assigned_to[]", [.. assignedTo.Select(id => id.ToString())]);
	}
}
