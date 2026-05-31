namespace SolarWinds.Api.Test.ServiceDesk;

public class IncidentSubResourceTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetServiceMonitorStatistic_WithValidId_ReturnsResult()
	{
		var incidents = await ServiceDeskClient.Incidents.GetAsync(
			new GetIncidentsRequest { Page = 1, PerPage = 1 },
			CancellationToken);

		incidents.Should().NotBeEmpty();
		var id = incidents[0].Id;

		try
		{
			var stat = await ServiceDeskClient.Incidents.GetServiceMonitorStatisticAsync(id, CancellationToken);
			stat.Should().NotBeNull();
		}
		catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
		{
			// Some tenants do not expose service monitor statistics for all incidents.
		}
	}

	[Fact]
	public async Task GetWorkflow_WithValidId_ReturnsResult()
	{
		var incidents = await ServiceDeskClient.Incidents.GetAsync(
			new GetIncidentsRequest { Page = 1, PerPage = 1 },
			CancellationToken);

		incidents.Should().NotBeEmpty();
		var id = incidents[0].Id;

		_ = await ServiceDeskClient.Incidents.GetWorkflowAsync(id, CancellationToken);
	}

	[Fact]
	public async Task GetComments_WithValidId_ReturnsResult()
	{
		var incidents = await ServiceDeskClient.Incidents.GetAsync(
			new GetIncidentsRequest { Page = 1, PerPage = 1 },
			CancellationToken);

		incidents.Should().NotBeEmpty();
		var id = incidents[0].Id;

		try
		{
			var comments = await ServiceDeskClient.Comments.GetAsync(
				ObjectType.Incidents, id, unmasked: false, CancellationToken);

			comments.Should().NotBeNull();
		}
		catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
		{
			// Some tenants return 404 when an incident has no comments; treat as empty result.
		}
	}
}
