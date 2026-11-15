namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Audit integration tests.
/// </summary>
[Trait("Category", "Integration")]
public class AuditTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Listing audits returns items.
	/// </summary>
	[Fact]
	public async Task GetAll_ReturnsItems()
	{
		var items = await RetryOnServerErrorAsync(() => ServiceDeskClient.Audits.GetAsync(CancellationToken));

		items.Should().NotBeNull();
		items.Should().NotBeEmpty();
	}
}
