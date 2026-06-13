using SolarWinds.Api.Orion;
using SolarWinds.Api.Queries;

namespace SolarWinds.Api.Test.Orion;

/// <summary>
/// Represents this type.
/// </summary>
public class PollerTests(ITestOutputHelper iTestOutputHelper) : TestWithOutput(iTestOutputHelper)
{

	/// <summary>
	/// Valid sql query returns items
	/// </summary>
	[Fact]
	public async Task Valid_SqlQuery_ReturnsItems()
	{
		var queryResponse = await OrionClient.SqlQueryAsync<Poller>(new SqlQuery
		{
			Sql = "SELECT Description, Uri, InstanceType, Enabled, PollerType FROM Orion.Pollers WHERE PollerID>@p ORDER BY PollerID WITH ROWS 1 TO 3 WITH TOTALROWS",
			Parameters = new Dictionary<string, object>
			{
				{ "p", 0 }
			}
		}, CancellationToken);
		queryResponse.Should().NotBeNull();
		queryResponse.Results.Should().NotBeEmpty();
	}

	/// <summary>
	/// Valid filtered query returns items
	/// </summary>
	[Fact]
	public async Task Valid_FilterQuery_ReturnsItems()
	{
		var queryResponse = await OrionClient.FilterQueryAsync(new FilterQuery<Poller>
		{
			OrderBy = nameof(Entity.Uri),
			Skip = 0,
			Take = 3,
		}, CancellationToken);
		queryResponse.Should().NotBeNull();
		queryResponse.Results.Should().NotBeEmpty();
	}
}
