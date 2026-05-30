namespace SolarWinds.Api.Test.ServiceDesk;

public class OtherAssetTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetAll_ReturnsItems()
	{
		var items = await ServiceDeskClient.OtherAssets.GetAsync(new GetOtherAssetsRequest(), CancellationToken);
		items.Should().NotBeNull();
		items.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetById_WithValidId_ReturnsItem()
	{
		var items = await ServiceDeskClient.OtherAssets.GetAsync(new GetOtherAssetsRequest(), CancellationToken);
		items.Should().NotBeEmpty();
		var id = items[0].Id;
		var item = await ServiceDeskClient.OtherAssets.GetAsync(id, ResponseLayout.Short, CancellationToken);
		item.Should().NotBeNull();
		item.Id.Should().Be(id);
	}
}

