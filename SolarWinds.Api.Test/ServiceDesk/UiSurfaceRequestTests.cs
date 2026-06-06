namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class UiSurfaceRequestTests
{
	/// <summary>
	/// Executes CustomViews_And_Metadata_UseExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task CustomViews_And_Metadata_UseExpectedQueryParameters()
	{
		const string responseJson = "{}";
		var capture = new CaptureHandler(responseJson);
		using var client = CreateClient(capture);
		var api = CreateApi<IUiCustomViews>(client);

		_ = await api.GetViewAsync("incidents", new UiCustomViewRequest { ReportId = 8992193, IsPortalMode = false }, CancellationToken.None);
		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/custom_views/incidents.json");
		var query = ParseQuery(capture.LastRequest.RequestUri);
		query["report_id"].Should().Be("8992193");
		query["is_portal_mode"].Should().Be("False");

		_ = await api.GetMetadataAsync("incidents", new UiCustomViewMetadataRequest { ReportId = 8992193, IsPortalMode = false }, CancellationToken.None);
		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/custom_views/incidents/metadata.json");

		_ = await api.GetViewAsync("users", new UiCustomViewRequest
		{
			PageParametersController = "users",
			PageParametersAction = "index",
			PageParametersEnabled = 1,
			PageParametersReportId = 8992244,
			ReportId = 8992244,
			IsPortalMode = false,
		}, CancellationToken.None);
		capture.LastRequest.Should().NotBeNull();
		var advancedQuery = ParseQuery(capture.LastRequest!.RequestUri!);
		advancedQuery["page_parameters[controller]"].Should().Be("users");
		advancedQuery["page_parameters[action]"].Should().Be("index");
		advancedQuery["page_parameters[enabled]"].Should().Be("1");
		advancedQuery["page_parameters[report_id]"].Should().Be("8992244");
	}

	/// <summary>
	/// Executes UiInfrastructure_Endpoints_UseExpectedQueryParameters_AndDeserialize.
	/// </summary>
	[Fact]
	public async Task UiInfrastructure_Endpoints_UseExpectedQueryParameters_AndDeserialize()
	{
		const string responseJson = "{}";
		var capture = new CaptureHandler(responseJson);
		using var client = CreateClient(capture);
		var api = CreateApi<IUiInfrastructure>(client);
		var dashboardsApi = CreateApi<IDashboards>(client);
		var widgetsApi = CreateApi<IWidgets>(client);

		_ = await api.GetCustomContextAsync(new UiCustomContextRequest { Context = "incidents", IsPortalMode = false }, CancellationToken.None);
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/custom.json");

		_ = await api.GetUrlFiltersAsync(new UiUrlFiltersRequest { Context = "incidents", QueryString = "report_id=8992193", IsPortalMode = false }, CancellationToken.None);
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/filters/url_filters.json");

		capture.ResponseContent = "[{\"id\":1862317,\"name\":\"Default\"}]";
		var dashboards = await dashboardsApi.GetAsync(new PortalModeRequest { IsPortalMode = false }, CancellationToken.None);
		dashboards.Should().ContainSingle();
		dashboards[0].Id.Should().Be(1862317);

		capture.ResponseContent = "{\"id\":1862317,\"name\":\"Default\"}";
		var dashboard = await dashboardsApi.GetAsync(1862317, new PortalModeRequest { IsPortalMode = false }, CancellationToken.None);
		dashboard.Id.Should().Be(1862317);

		capture.ResponseContent = "{\"id\":24718108,\"type\":\"incidents_by_type\"}";
		var widget = await widgetsApi.GetAsync(24718108, new UiWidgetRequest { IsPortalMode = false, ElsEnabled = false, NumOfUpdates = 0 }, CancellationToken.None);
		widget.Id.Should().Be(24718108);

		capture.ResponseContent = "{\"points\":[1,2,3]}";
		var widgetData = await widgetsApi.GetDataAsync(new UiWidgetDataRequest
		{
			Type = "incidents_by_type",
			Settings = "{}",
			WidgetId = 24718108,
			Filters = string.Empty,
			ConnectionId = string.Empty,
			IsPortalMode = false,
		}, CancellationToken.None);
		widgetData.Should().NotBeNull();

		capture.ResponseContent = "{\"jwt\":\"abc\"}";
		var jwt = await api.GetWebsocketJwtAsync(new PortalModeRequest { IsPortalMode = false }, CancellationToken.None);
		jwt.ToString().Should().Contain("abc");

		capture.ResponseContent = "{}";
		_ = await api.GetRumScriptAsync(new PortalModeRequest { IsPortalMode = false }, CancellationToken.None);
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/rum_script.json");
	}

	/// <summary>
	/// Executes UsersAvatars_And_ChangeCatalogsTree_UseExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task UsersAvatars_And_ChangeCatalogsTree_UseExpectedQueryParameters()
	{
		var capture = new CaptureHandler("[]");
		using var client = CreateClient(capture);

		var usersApi = CreateApi<IUsers>(client);
		_ = await usersApi.GetAvatarsAsync([14959623, 14959585], false, CancellationToken.None);
		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/users/get_avatars.json");
		var rawQuery = Uri.UnescapeDataString(capture.LastRequest.RequestUri.Query);
		rawQuery.Should().Contain("userIds[]=14959623");
		rawQuery.Should().Contain("userIds[]=14959585");
		rawQuery.Should().Contain("is_portal_mode=False");

		_ = await usersApi.GetAsync(new GetUsersRequest { Enabled = 1, ReportId = 8992244, IsPortalMode = false }, CancellationToken.None);
		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/users.json");
		var usersQuery = ParseQuery(capture.LastRequest.RequestUri);
		usersQuery["enabled"].Should().Be("1");
		usersQuery["report_id"].Should().Be("8992244");
		usersQuery["is_portal_mode"].Should().Be("False");

		var ccApi = CreateApi<IChangeCatalogs>(client);
		_ = await ccApi.GetAsync(new GetChangeCatalogsRequest { Type = "tree", NewButton = true, State = 3, IsPortalMode = false }, CancellationToken.None);
		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/change_catalogs.json");
		var ccQuery = ParseQuery(capture.LastRequest.RequestUri);
		ccQuery["type"].Should().Be("tree");
		ccQuery["newButton"].Should().Be("True");
		ccQuery["state"].Should().Be("3");
		ccQuery["is_portal_mode"].Should().Be("False");

		var listsApi = CreateApi<IUiJsonHtmlLists>(client);
		_ = await listsApi.GetAsync("incidents", new UiJsonHtmlRequest { ReportId = 8992193 }, CancellationToken.None);
		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/incidents.jsonhtml");

		var customFormsApi = CreateApi<ICustomForms>(client);
		_ = await customFormsApi.GetSetupListAsync(new UiCustomViewRequest { ReportId = 9298777, IsPortalMode = false }, CancellationToken.None);
		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/setup/custom_forms.json");

		var templatesApi = CreateApi<IResponseTemplates>(client);
		_ = await templatesApi.GetSetupListAsync(new PortalModeRequest { IsPortalMode = false }, CancellationToken.None);
		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/setup/response_templates.json");
	}

	private static HttpClient CreateClient(HttpMessageHandler handler) => new(handler) { BaseAddress = new Uri("https://api.samanage.com") };

	private static T CreateApi<T>(HttpClient client) where T : class
		=> RestService.For<T>(client, new RefitSettings(
			new SystemTextJsonContentSerializer(new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower }))
		{
			UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
		});

	private static Dictionary<string, string> ParseQuery(Uri uri)
	{
		var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		var query = uri.Query;
		if (string.IsNullOrWhiteSpace(query)) return result;
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
		public string ResponseContent { get; set; } = responseContent;

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			LastRequest = request;
			return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent(ResponseContent, Encoding.UTF8, "application/json")
			});
		}
	}
}
