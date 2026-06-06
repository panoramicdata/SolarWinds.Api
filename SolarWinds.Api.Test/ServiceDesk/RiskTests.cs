namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class RiskTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Executes GetAll_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_ReturnsItems()
	{
		var items = await ServiceDeskClient.Risks.GetAsync(CancellationToken);
		items.Should().NotBeNull();
		items.Should().NotBeEmpty();
	}
}
