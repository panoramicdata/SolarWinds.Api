namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class ChangeTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Executes GetAll_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_ReturnsItems()
	{
		var items = await ServiceDeskClient.Changes.GetAsync(new GetChangesRequest(), CancellationToken);
		items.Should().NotBeNull();
		items.Should().NotBeEmpty();
	}

	/// <summary>
	/// Executes GetById_WithValidId_ReturnsItem.
	/// </summary>
	[Fact]
	public async Task GetById_WithValidId_ReturnsItem()
	{
		var items = await ServiceDeskClient.Changes.GetAsync(new GetChangesRequest(), CancellationToken);
		items.Should().NotBeEmpty();
		var id = items[0].Id;
		var item = await ServiceDeskClient.Changes.GetAsync(id, ResponseLayout.Short, CancellationToken);
		item.Should().NotBeNull();
		item.Id.Should().Be(id);
	}
}

