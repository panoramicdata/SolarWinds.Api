namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Tests the query string built for incident requests.
/// </summary>
public class IncidentQueryRequestTests
{
	private static Task<HttpRequestMessage> CaptureAsync(GetIncidentsRequest request)
		=> ServiceDeskTestApi.CaptureAsync<IIncidents>(api => api.GetAsync(request, CancellationToken.None));

	/// <summary>
	/// An empty request omits the optional parameters.
	/// </summary>
	[Fact]
	public async Task GetAll_WithEmptyRequest_OmitsOptionalQueryParameters()
	{
		var request = await CaptureAsync(new GetIncidentsRequest());

		var query = ServiceDeskTestApi.AssertGet(request, "/incidents.json", ("layout", "short"));
		query.Keys.Should().NotContain(
		[
			"updated",
			"updated_from",
			"updated_to",
			"updated_custom_gte",
			"updated_custom_lte",
			"created_from",
			"created_to",
			"page",
			"per_page"
		]);
	}

	/// <summary>
	/// A long layout with an updated range is sent as the expected parameters.
	/// </summary>
	[Fact]
	public async Task GetAll_WithLayoutLongAndUpdatedRange_UsesExpectedQueryParameters()
	{
		var request = await CaptureAsync(new GetIncidentsRequest
		{
			Layout = ResponseLayout.Long,
			Updated = "Select Date Range",
			UpdatedCustomGte = new DateTime(2023, 3, 2),
			UpdatedCustomLte = new DateTime(2023, 3, 9),
			Page = 2,
			PerPage = 100
		});

		ServiceDeskTestApi.AssertGet(
			request,
			"/incidents.json",
			("layout", "long"),
			("updated", "Select Date Range"),
			("updated_custom_gte", "Mar 2 2023"),
			("updated_custom_lte", "Mar 9 2023"),
			("page", "2"),
			("per_page", "100"));
	}

	/// <summary>
	/// An updated from/to window is sent as dates without the range selector.
	/// </summary>
	[Fact]
	public async Task GetAll_WithUpdatedFromTo_UsesExpectedQueryParameters()
	{
		var request = await CaptureAsync(new GetIncidentsRequest
		{
			Layout = ResponseLayout.Long,
			UpdatedFrom = new DateTime(2023, 11, 29, 8, 0, 0),
			UpdatedTo = new DateTime(2023, 11, 30, 8, 0, 0),
		});

		var query = ServiceDeskTestApi.AssertGet(
			request,
			"/incidents.json",
			("layout", "long"),
			("updated_from", "Nov 29 2023"),
			("updated_to", "Nov 30 2023"));
		query.Should().NotContainKey("updated");
	}

	/// <summary>
	/// Portal search parameters are sent as the expected query parameters.
	/// </summary>
	[Fact]
	public async Task GetAll_WithPortalSearchParameters_UsesExpectedQueryParameters()
	{
		const string columns = "number,slm,sla_next_timestamp,state,preview,title,priority,origin,type,sub_type,assigned_to,requester,site,department,reassignments,asset,cc,created_by,customer_satisfaction_feedback,closed_at,changes,created_at,updated_at_with_time,updated_at,total_time_spent,to_resolve_elapsed,to_resolve,to_first_response_elapsed,to_first_response,to_close,to_assignment,incident_slash_service_request,tag_list,scheduled,resolved_by,resolved_at,resolution_type,resolution,problems,price,pending_approval,object_id,last_state_change,last_group_reassigned,last_reassigned,assigned_to_group,due_date,description,customer_satisfied?,created_at_with_time";

		var request = await CaptureAsync(new GetIncidentsRequest
		{
			ReportId = 8992193,
			Applied = true,
			Department = [1143037, 1143039, 1143040],
			Description = ["printer"],
			DescriptionIsNot = ["w00"],
			Title = ["prin?er"],
			SortBy = "title",
			SortOrder = "DESC",
			Columns = columns,
			SearchInContext = "printer",
			RequestId = "482d079acd55fa66278f",
			ConnectionId = "eMdXLeRxoAMCEuw="
		});

		var query = ServiceDeskTestApi.AssertGet(
			request,
			"/incidents.json",
			("report_id", "8992193"),
			("applied", "True"),
			("description[]", "printer"),
			("description_is_not[]", "w00"),
			("title[]", "prin?er"),
			("sort_by", "title"),
			("sort_order", "DESC"),
			("columns", columns),
			("search_in_context", "printer"),
			("requestId", "482d079acd55fa66278f"),
			("connectionId", "eMdXLeRxoAMCEuw="));
		query["columns"].Should().Contain("customer_satisfied?");

		// Each department must be its own department[] parameter, not one joined value.
		ServiceDeskTestApi.AssertRepeated(request, "department[]", "1143037", "1143039", "1143040");
	}

	/// <summary>
	/// A title filter is sent as a repeated title parameter.
	/// </summary>
	[Fact]
	public async Task GetAll_WithTitleQuery_UsesExpectedQueryParameters()
	{
		var request = await CaptureAsync(new GetIncidentsRequest
		{
			Title = ["New Laptop Request"]
		});

		ServiceDeskTestApi.AssertGet(request, "/incidents.json", ("layout", "short"), ("title[]", "New Laptop Request"));
	}
}
