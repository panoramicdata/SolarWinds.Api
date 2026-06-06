namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class IncidentTypeTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Executes GetTypesList_ReturnsResult.
	/// </summary>
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
