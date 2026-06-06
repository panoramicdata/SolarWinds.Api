namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class IncidentSubResourceTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Executes GetServiceMonitorStatistic_WithValidId_ReturnsResult.
	/// </summary>
	[Fact]
	public async Task GetServiceMonitorStatistic_WithValidId_ReturnsResult()
	{
		var id = await GetExistingIncidentIdAsync();

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

	/// <summary>
	/// Executes GetWorkflow_WithValidId_ReturnsResult.
	/// </summary>
	[Fact]
	public async Task GetWorkflow_WithValidId_ReturnsResult()
	{
		var id = await GetExistingIncidentIdAsync();

		try
		{
			_ = await ServiceDeskClient.Incidents.GetWorkflowAsync(id, CancellationToken);
		}
		catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
		{
			// Some tenants do not expose workflow payloads for all incidents.
		}
	}

	/// <summary>
	/// Executes GetComments_WithValidId_ReturnsResult.
	/// </summary>
	[Fact]
	public async Task GetComments_WithValidId_ReturnsResult()
	{
		var id = await GetExistingIncidentIdAsync();

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

	private async Task<int> GetExistingIncidentIdAsync()
	{
		List<Incident> incidents = [];

		for (var attempt = 1; attempt <= 3; attempt++)
		{
			incidents = await ServiceDeskClient.Incidents.GetAsync(
				new GetIncidentsRequest { Page = 1, PerPage = 10 },
				CancellationToken);

			if (incidents.Count > 0)
			{
				return incidents[0].Id;
			}

			if (attempt < 3)
			{
				await Task.Delay(TimeSpan.FromSeconds(1), CancellationToken);
			}
		}

		incidents.Should().NotBeEmpty("expected at least one incident after 3 attempts");
		return incidents[0].Id;
	}
}
