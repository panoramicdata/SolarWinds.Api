namespace SolarWinds.Api.Test.ServiceDesk;

public class ResponseTemplateTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetTotalCount_ReturnsResult()
	{
		var result = await ServiceDeskClient.ResponseTemplates.GetTotalCountAsync(CancellationToken);
		result.Should().NotBeNull();
		result.EffectiveCount.Should().BeGreaterThanOrEqualTo(0);
	}

	[Fact]
	public async Task GetTotalCountPersonal_ReturnsResult()
	{
		var result = await ServiceDeskClient.ResponseTemplates.GetTotalCountPersonalAsync(CancellationToken);
		result.Should().NotBeNull();
		result.EffectiveCount.Should().BeGreaterThanOrEqualTo(0);
	}

	[Fact]
	public async Task GetTotalCountGlobal_ReturnsResult()
	{
		var result = await ServiceDeskClient.ResponseTemplates.GetTotalCountGlobalAsync(CancellationToken);
		result.Should().NotBeNull();
		result.EffectiveCount.Should().BeGreaterThanOrEqualTo(0);
	}

	[Fact]
	public async Task GetResponseTemplateVariables_WithValidIncidentId_ReturnsResult()
	{
		var incidents = await ServiceDeskClient.Incidents.GetAsync(
			new GetIncidentsRequest { Page = 1, PerPage = 1 },
			CancellationToken);

		incidents.Should().NotBeEmpty();
		var incidentId = incidents[0].Id;

		var variables = await ServiceDeskClient.Incidents.GetResponseTemplateVariablesAsync(incidentId, CancellationToken);
		variables.Should().NotBeNull();
	}
}
