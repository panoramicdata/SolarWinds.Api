namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class DepartmentTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Executes GetAll_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_ReturnsItems()
	{
		var items = await ServiceDeskClient.Departments.GetAsync(CancellationToken);
		items.Should().NotBeNull();
		items.Should().NotBeEmpty();
	}

	/// <summary>
	/// Executes GetById_WithValidId_ReturnsItem.
	/// </summary>
	[Fact]
	public async Task GetById_WithValidId_ReturnsItem()
	{
		var items = await ServiceDeskClient.Departments.GetAsync(CancellationToken);
		items.Should().NotBeEmpty();
		var id = items[0].Id;
		var item = await ServiceDeskClient.Departments.GetAsync(id, ResponseLayout.Short, CancellationToken);
		item.Should().NotBeNull();
		item.Id.Should().Be(id);
	}
}

