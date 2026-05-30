using Xunit;
using Xunit.Abstractions;
using AwesomeAssertions;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.Test.ServiceDesk;

public class CategoryTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetAll_ReturnsItems()
	{
		var items = await ServiceDeskClient.Categories.GetAsync(new GetCategoriesRequest(), CancellationToken);
		items.Should().NotBeNull();
		items.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetById_WithValidId_ReturnsItem()
	{
		var items = await ServiceDeskClient.Categories.GetAsync(new GetCategoriesRequest(), CancellationToken);
		items.Should().NotBeEmpty();
		var id = items[0].Id;
		var item = await ServiceDeskClient.Categories.GetAsync(id, ResponseLayout.Short, CancellationToken);
		item.Should().NotBeNull();
		item.Id.Should().Be(id);
	}
}

