using System.Threading.Tasks;
using SolarWinds.Api.Test;
using Xunit;
using Xunit.Abstractions;
using AwesomeAssertions;

namespace SolarWinds.Api.Test.ServiceDesk;

public class RiskTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetAll_ReturnsItems()
	{
		var items = await ServiceDeskClient.Risks.GetAllAsync(CancellationToken);
		items.Should().NotBeNull();
		items.Should().NotBeEmpty();
	}
}
