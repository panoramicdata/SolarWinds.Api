namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class SoftwareTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Executes GetAll_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_ReturnsItems()
	{
		var items = await ServiceDeskClient.Softwares.GetAsync(CancellationToken);
		items.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetById_WithValidId_ReturnsItem.
	/// </summary>
	[Fact]
	public async Task GetById_WithValidId_ReturnsItem()
	{
		var items = await ServiceDeskClient.Softwares.GetAsync(CancellationToken);
		if (items.Count == 0)
		{
			return;
		}
		var id = items[0].Id;
		var item = await ServiceDeskClient.Softwares.GetAsync(id, ResponseLayout.Short, CancellationToken);
		item.Should().NotBeNull();
		item.Id.Should().Be(id);
	}
}

