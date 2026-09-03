namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class TaskQueryRequestTests
{
	/// <summary>
	/// Executes GetAll_WithPortalSearchParameters_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task GetAll_WithPortalSearchParameters_UsesExpectedQueryParameters()
	{
		const string columns = "type,title,assigned_to,requester,source,due_date";
		const string assignedTo = "16453692,16453698,16453699,16453703,16453704,16453705,16453706,16453707,16453731";

		var capture = new CaptureHandler();
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);

		var tasksApi = ServiceDeskTestApi.CreateApi<ITasks>(client);

		await tasksApi.GetAsync(new GetTasksRequest
		{
			ReportId = 8992259,
			Applied = true,
			AssignedTo = [16453692, 16453698, 16453699, 16453703, 16453704, 16453705, 16453706, 16453707, 16453731],
			Columns = columns,
		}, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.Method.Should().Be(HttpMethod.Get);
		capture.LastRequest.RequestUri.Should().NotBeNull();
		capture.LastRequest.RequestUri!.AbsolutePath.Should().Be("/tasks.json");

		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query.Should().ContainKey("report_id");
		query["report_id"].Should().Be("8992259");
		query.Should().ContainKey("applied");
		query["applied"].Should().Be("True");
		// Service Desk takes a multi-valued filter as a repeated key, Rails-style, so each assignee
		// must appear as its own assigned_to[] parameter rather than as one joined value.
		var queryValues = ServiceDeskTestApi.ParseQueryValues(capture.LastRequest.RequestUri);
		queryValues.Should().ContainKey("assigned_to[]");
		queryValues["assigned_to[]"].Should().Equal(assignedTo.Split(','));
		query.Should().ContainKey("columns");
		query["columns"].Should().Be(columns);
	}

}
