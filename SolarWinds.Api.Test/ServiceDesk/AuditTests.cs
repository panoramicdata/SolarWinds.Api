using Xunit;
using Xunit.Abstractions;
using AwesomeAssertions;

namespace SolarWinds.Api.Test.ServiceDesk;

public class AuditTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetAll_ReturnsItems()
	{
		var items = await ServiceDeskClient.Audits.GetAsync(CancellationToken);
		items.Should().NotBeNull();
		items.Should().NotBeEmpty();
	}
}
