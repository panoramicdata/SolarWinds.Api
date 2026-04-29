using System.Threading.Tasks;
using SolarWinds.Api.Test;
using Xunit;
using Xunit.Abstractions;
using AwesomeAssertions;

namespace SolarWinds.Api.Test.ServiceDesk;

public class HardwareTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetAll_ReturnsCollection()
	{
		var items = await ServiceDeskClient.Hardwares.GetAllAsync(CancellationToken);
		items.Should().NotBeNull();
	}

	[Fact]
	public async Task GetById_WhenItemsExist_ReturnsItem()
	{
		var items = await ServiceDeskClient.Hardwares.GetAllAsync(CancellationToken);
		items.Should().NotBeNull();
		if (items.Count == 0)
		{
			return;
		}

		var id = items[0].Id;
		var item = await ServiceDeskClient.Hardwares.GetAsync(id, CancellationToken);
		item.Should().NotBeNull();
		item.Id.Should().Be(id);
	}
}
