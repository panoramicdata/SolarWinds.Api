namespace SolarWinds.Api.Test.ServiceDesk;

public class IncidentQueryRequestTests
{
	[Fact]
	public async Task GetAll_WithEmptyRequest_OmitsOptionalQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = new HttpClient(capture)
		{
			BaseAddress = new Uri("https://api.samanage.com")
		};

		var incidentsApi = RestService.For<IIncidents>(client, new RefitSettings(
			new SystemTextJsonContentSerializer(new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
			}))
		{
			UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
		});

		await incidentsApi.GetAsync(new GetIncidentsRequest(), CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/incidents.json");
		var query = ParseQuery(capture.LastRequest.RequestUri);
		query.Should().ContainKey("layout");
		query["layout"].Should().Be("short");
		query.Should().NotContainKey("updated");
		query.Should().NotContainKey("updated_from");
		query.Should().NotContainKey("updated_to");
		query.Should().NotContainKey("updated_custom_gte");
		query.Should().NotContainKey("updated_custom_lte");
		query.Should().NotContainKey("created_from");
		query.Should().NotContainKey("created_to");
		query.Should().NotContainKey("page");
		query.Should().NotContainKey("per_page");
	}

	[Fact]
	public async Task GetAll_WithLayoutLongAndUpdatedRange_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = new HttpClient(capture)
		{
			BaseAddress = new Uri("https://api.samanage.com")
		};

		var incidentsApi = RestService.For<IIncidents>(client, new RefitSettings(
			new SystemTextJsonContentSerializer(new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
			}))
		{
			UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
		});

		await incidentsApi.GetAsync(new GetIncidentsRequest
		{
			Layout = ResponseLayout.Long,
			Updated = "Select Date Range",
			UpdatedCustomGte = new DateTime(2023, 3, 2),
			UpdatedCustomLte = new DateTime(2023, 3, 9),
			Page = 2,
			PerPage = 100
		}, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.Method.Should().Be(HttpMethod.Get);
		capture.LastRequest.RequestUri.Should().NotBeNull();
		capture.LastRequest.RequestUri!.AbsolutePath.Should().Be("/incidents.json");

		var query = ParseQuery(capture.LastRequest.RequestUri);
		query.Should().ContainKey("layout");
		query["layout"].Should().Be("long");
		query.Should().ContainKey("updated");
		query["updated"].Should().Be("Select Date Range");
		query.Should().ContainKey("updated_custom_gte");
		query["updated_custom_gte"].Should().Be("Mar 2 2023");
		query.Should().ContainKey("updated_custom_lte");
		query["updated_custom_lte"].Should().Be("Mar 9 2023");
		query.Should().ContainKey("page");
		query["page"].Should().Be("2");
		query.Should().ContainKey("per_page");
		query["per_page"].Should().Be("100");
	}

	[Fact]
	public async Task GetAll_WithUpdatedFromTo_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = new HttpClient(capture)
		{
			BaseAddress = new Uri("https://api.samanage.com")
		};

		var incidentsApi = RestService.For<IIncidents>(client, new RefitSettings(
			new SystemTextJsonContentSerializer(new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
			}))
		{
			UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
		});

		await incidentsApi.GetAsync(new GetIncidentsRequest
		{
			Layout = ResponseLayout.Long,
			UpdatedFrom = new DateTime(2023, 11, 29, 8, 0, 0),
			UpdatedTo = new DateTime(2023, 11, 30, 8, 0, 0),
		}, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		var query = ParseQuery(capture.LastRequest!.RequestUri!);

		query.Should().ContainKey("layout");
		query["layout"].Should().Be("long");
		query.Should().ContainKey("updated_from");
		query["updated_from"].Should().Be("Nov 29 2023");
		query.Should().ContainKey("updated_to");
		query["updated_to"].Should().Be("Nov 30 2023");
		query.Should().NotContainKey("updated");
	}

	[Fact]
	public async Task GetAll_WithPortalSearchParameters_UsesExpectedQueryParameters()
	{
		const string columns = "number,slm,sla_next_timestamp,state,preview,title,priority,origin,type,sub_type,assigned_to,requester,site,department,reassignments,asset,cc,created_by,customer_satisfaction_feedback,closed_at,changes,created_at,updated_at_with_time,updated_at,total_time_spent,to_resolve_elapsed,to_resolve,to_first_response_elapsed,to_first_response,to_close,to_assignment,incident_slash_service_request,tag_list,scheduled,resolved_by,resolved_at,resolution_type,resolution,problems,price,pending_approval,object_id,last_state_change,last_group_reassigned,last_reassigned,assigned_to_group,due_date,description,customer_satisfied?,created_at_with_time";

		var capture = new CaptureHandler();
		using var client = new HttpClient(capture)
		{
			BaseAddress = new Uri("https://api.samanage.com")
		};

		var incidentsApi = RestService.For<IIncidents>(client, new RefitSettings(
			new SystemTextJsonContentSerializer(new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
			}))
		{
			UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
		});

		await incidentsApi.GetAsync(new GetIncidentsRequest
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
		}, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		var query = ParseQuery(capture.LastRequest!.RequestUri!);

		query.Should().ContainKey("report_id");
		query["report_id"].Should().Be("8992193");
		query.Should().ContainKey("applied");
		query["applied"].Should().Be("True");
		query.Should().ContainKey("description[]");
		query["description[]"].Should().Be("printer");
		query.Should().ContainKey("description_is_not[]");
		query["description_is_not[]"].Should().Be("w00");
		query.Should().ContainKey("title[]");
		query["title[]"].Should().Be("prin?er");

		query.Should().ContainKey("department[]");
		query["department[]"].Should().Be("1143037,1143039,1143040");

		query.Should().ContainKey("sort_by");
		query["sort_by"].Should().Be("title");
		query.Should().ContainKey("sort_order");
		query["sort_order"].Should().Be("DESC");
		query.Should().ContainKey("columns");
		query["columns"].Should().Be(columns);
		query["columns"].Should().Contain("customer_satisfied?");
		query.Should().ContainKey("search_in_context");
		query["search_in_context"].Should().Be("printer");
		query.Should().ContainKey("requestId");
		query["requestId"].Should().Be("482d079acd55fa66278f");
		query.Should().ContainKey("connectionId");
		query["connectionId"].Should().Be("eMdXLeRxoAMCEuw=");
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

	private sealed class CaptureHandler : HttpMessageHandler
	{
		public HttpRequestMessage? LastRequest { get; private set; }

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			LastRequest = request;
			var response = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent("[]")
			};

			return Task.FromResult(response);
		}
	}
}