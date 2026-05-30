using Xunit;
using Xunit.Abstractions;
using AwesomeAssertions;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.Test.ServiceDesk;

public class IncidentTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetAll_ReturnsItems()
	{
		var items = await ServiceDeskClient.Incidents.GetAsync(new GetIncidentsRequest
		{
			Page = 1,
			PerPage = 10
		}, CancellationToken);
		items.Should().NotBeNull();
		items.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetById_WithValidId_ReturnsItem()
	{
		var items = await ServiceDeskClient.Incidents.GetAsync(new GetIncidentsRequest
		{
			Page = 1,
			PerPage = 10
		}, CancellationToken);
		items.Should().NotBeEmpty();
		var id = items[0].Id;
		var item = await ServiceDeskClient.Incidents.GetAsync(id, ResponseLayout.Short, CancellationToken);
		item.Should().NotBeNull();
		item.Id.Should().Be(id);
	}

	[Fact]
	public async Task GetEntityGeneralInfo_WithValidId_ReturnsStates()
	{
		var items = await ServiceDeskClient.Incidents.GetAsync(new GetIncidentsRequest
		{
			Page = 1,
			PerPage = 10
		}, CancellationToken);

		items.Should().NotBeEmpty();
		var id = items[0].Id;

		var info = await ServiceDeskClient.Incidents.GetEntityGeneralInfoAsync(id, CancellationToken);

		info.Should().NotBeNull();
		info.States.Should().NotBeNull();
		info.States.Should().NotBeEmpty();
		info.States.Should().Contain(state => state.Id > 0);
		info.States.Should().Contain(state => state.Selected);
	}
}

