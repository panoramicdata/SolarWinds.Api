using AwesomeAssertions;
using Xunit;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test.ServiceDesk;

public class IncidentTypeTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetTypesList_ReturnsResult()
	{
		var items = await ServiceDeskClient.IncidentTypes.GetTypesListAsync(
			new GetIncidentTypesRequest
			{
				Page = 1,
				PerPage = 25,
				Model = "Incident",
			},
			CancellationToken);

		items.Should().NotBeNull();
	}
}
