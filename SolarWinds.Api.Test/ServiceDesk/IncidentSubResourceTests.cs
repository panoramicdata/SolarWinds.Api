using AwesomeAssertions;
using Xunit;
using Xunit.Abstractions;

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

		var stat = await ServiceDeskClient.Incidents.GetServiceMonitorStatisticAsync(id, CancellationToken);
		stat.Should().NotBeNull();
	}

	[Fact]
	public async Task GetWorkflow_WithValidId_ReturnsResult()
	{
		var incidents = await ServiceDeskClient.Incidents.GetAsync(
			new GetIncidentsRequest { Page = 1, PerPage = 1 },
			CancellationToken);

		incidents.Should().NotBeEmpty();
		var id = incidents[0].Id;

		var workflow = await ServiceDeskClient.Incidents.GetWorkflowAsync(id, CancellationToken);
		workflow.Should().NotBeNull();
	}

	[Fact]
	public async Task GetComments_WithValidId_ReturnsResult()
	{
		var incidents = await ServiceDeskClient.Incidents.GetAsync(
			new GetIncidentsRequest { Page = 1, PerPage = 1 },
			CancellationToken);

		incidents.Should().NotBeEmpty();
		var id = incidents[0].Id;

		var comments = await ServiceDeskClient.Comments.GetAsync(
			ObjectType.Incidents, id, unmasked: false, CancellationToken);

		comments.Should().NotBeNull();
	}
}
