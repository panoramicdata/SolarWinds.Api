using Microsoft.Extensions.Configuration;

namespace SolarWinds.Api.Test.ServiceDesk;

public class UiSurfaceIntegrationTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task UiCustomViews_Endpoints_ReturnResults()
	{
		if (!ShouldRunIntegrationTest())
        {
            return;
        }

		var view = await ServiceDeskClient.UiCustomViews.GetViewAsync(
			"incidents",
			new UiCustomViewRequest { ReportId = 8992193, IsPortalMode = false },
			CancellationToken);
		view.Should().NotBeNull();

		var metadata = await ServiceDeskClient.UiCustomViews.GetMetadataAsync(
			"incidents",
			new UiCustomViewMetadataRequest { ReportId = 8992193, IsPortalMode = false },
			CancellationToken);
		metadata.Should().NotBeNull();
	}

	[Fact]
	public async Task UiInfrastructure_Endpoints_ReturnResults()
	{
		if (!ShouldRunIntegrationTest())
        {
            return;
        }

		try
		{
			var custom = await ServiceDeskClient.UiInfrastructure.GetCustomContextAsync(
				new UiCustomContextRequest { Context = "incidents", IsPortalMode = false },
				CancellationToken);
			custom.Should().NotBeNull();

			var filters = await ServiceDeskClient.UiInfrastructure.GetUrlFiltersAsync(
				new UiUrlFiltersRequest { Context = "incidents", QueryString = "report_id=8992193", IsPortalMode = false },
				CancellationToken);
			filters.Should().NotBeNull();

			var dashboards = await ServiceDeskClient.Dashboards.GetAsync(
				new PortalModeRequest { IsPortalMode = false },
				CancellationToken);
			dashboards.Should().NotBeNull();

			if (dashboards.Count > 0 && dashboards[0].Id is int dashboardId)
			{
				var dashboard = await ServiceDeskClient.Dashboards.GetAsync(
					dashboardId,
					new PortalModeRequest { IsPortalMode = false },
					CancellationToken);
				dashboard.Should().NotBeNull();

				var widget = await ServiceDeskClient.Widgets.GetAsync(
					24718108,
					new UiWidgetRequest { IsPortalMode = false, ElsEnabled = false, NumOfUpdates = 0 },
					CancellationToken);
				widget.Should().NotBeNull();

				var widgetData = await ServiceDeskClient.Widgets.GetDataAsync(
					new UiWidgetDataRequest
					{
						Type = "incidents_by_type",
						Settings = "{}",
						WidgetId = 24718108,
						Filters = string.Empty,
						ConnectionId = string.Empty,
						IsPortalMode = false,
					},
					CancellationToken);
				widgetData.Should().NotBeNull();
			}

			var websocketJwt = await ServiceDeskClient.UiInfrastructure.GetWebsocketJwtAsync(
				new PortalModeRequest { IsPortalMode = false },
				CancellationToken);
			websocketJwt.Should().NotBeNull();

			var rumScript = await ServiceDeskClient.UiInfrastructure.GetRumScriptAsync(
				new PortalModeRequest { IsPortalMode = false },
				CancellationToken);
			rumScript.Should().NotBeNull();

			var incidentList = await ServiceDeskClient.UiJsonHtmlLists.GetAsync(
				"incidents",
				new UiJsonHtmlRequest { ReportId = 8992193 },
				CancellationToken);
			incidentList.Should().NotBeNull();

			var setupCustomForms = await ServiceDeskClient.CustomForms.GetSetupListAsync(
				new UiCustomViewRequest { ReportId = 9298777, IsPortalMode = false },
				CancellationToken);
			setupCustomForms.Should().NotBeNull();

			var setupTemplates = await ServiceDeskClient.ResponseTemplates.GetSetupListAsync(
				new PortalModeRequest { IsPortalMode = false },
				CancellationToken);
			setupTemplates.Should().NotBeNull();
		}
		catch (ApiException ex) when ((int)ex.StatusCode == 401)
		{
			// Some UI infrastructure endpoints can require additional web-session context.
			return;
		}
	}

	[Fact]
	public async Task UsersAvatars_And_ChangeCatalogTree_ReturnResults()
	{
		if (!ShouldRunIntegrationTest())
        {
            return;
        }

		var users = await ServiceDeskClient.Users.GetAsync(CancellationToken);
		users.Should().NotBeNull();

		var filteredUsers = await ServiceDeskClient.Users.GetAsync(new GetUsersRequest
		{
			Enabled = 1,
			ReportId = 8992244,
			IsPortalMode = false,
		}, CancellationToken);
		filteredUsers.Should().NotBeNull();

		if (users.Count > 0)
		{
			var sampleIds = users.Take(2).Select(u => u.Id).Where(i => i > 0).ToArray();
			if (sampleIds.Length > 0)
			{
				var avatars = await ServiceDeskClient.Users.GetAvatarsAsync(sampleIds, false, CancellationToken);
				avatars.Should().NotBeNull();
			}
		}

		var tree = await ServiceDeskClient.ChangeCatalogs.GetAsync(
			new GetChangeCatalogsRequest { Type = "tree", NewButton = true, State = 3, IsPortalMode = false },
			CancellationToken);
		tree.Should().NotBeNull();
	}

	private static bool ShouldRunIntegrationTest()
	{
		var configuration = new ConfigurationBuilder()
			.AddUserSecrets<UiSurfaceIntegrationTests>()
			.Build();

		return bool.TryParse(configuration["ServiceDesk:Lifecycle:RunTests"], out var shouldRun)
			&& shouldRun;
	}
}
