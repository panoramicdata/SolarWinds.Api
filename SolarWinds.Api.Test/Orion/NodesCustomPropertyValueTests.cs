using System.Threading.Tasks;
using AwesomeAssertions;
using SolarWinds.Api.Orion;
using SolarWinds.Api.Queries;
using Xunit;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test.Orion;

public class NodesCustomPropertyTests(ITestOutputHelper iTestOutputHelper) : TestWithOutput(iTestOutputHelper)
{

	/// <summary>
	/// Valid SQL query returns items
	/// </summary>
	[Fact]
	public async Task Valid_SqlQuery_ReturnsItems()
	{
		var queryResponse = await Client.SqlQueryAsync<NodesCustomProperty>(new SqlQuery
		{
			Sql = "SELECT DisplayName FROM Orion.NodesCustomProperties ORDER BY Uri WITH ROWS 1 TO 3 WITH TOTALROWS"
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
		var queryResponse = await Client.FilterQueryAsync(new FilterQuery<NodesCustomProperty>
		{
			OrderBy = nameof(Entity.Uri),
			Skip = 0,
			Take = 3,
		}, CancellationToken);
		queryResponse.Should().NotBeNull();
		queryResponse.Results.Should().NotBeEmpty();
	}
}
