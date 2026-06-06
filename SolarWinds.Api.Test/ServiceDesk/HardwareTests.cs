namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class HardwareTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Executes GetAll_ReturnsCollection.
	/// </summary>
	[Fact]
	public async Task GetAll_ReturnsCollection()
	{
		var items = await ServiceDeskClient.Hardwares.GetAsync(new GetHardwaresRequest(), CancellationToken);
		items.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetById_WhenItemsExist_ReturnsItem.
	/// </summary>
	[Fact]
	public async Task GetById_WhenItemsExist_ReturnsItem()
	{
		var items = await ServiceDeskClient.Hardwares.GetAsync(new GetHardwaresRequest(), CancellationToken);
		items.Should().NotBeNull();
		if (items.Count == 0)
		{
			return;
		}

		var id = items[0].Id;
		var item = await ServiceDeskClient.Hardwares.GetAsync(id, ResponseLayout.Short, CancellationToken);
		item.Should().NotBeNull();
		item.Id.Should().Be(id);
	}
}

