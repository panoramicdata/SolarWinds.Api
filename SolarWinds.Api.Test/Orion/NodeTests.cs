using SolarWinds.Api.Orion;
using SolarWinds.Api.Queries;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using System.Threading;
using AwesomeAssertions;

namespace SolarWinds.Api.Test.Orion;

public class NodeTests(ITestOutputHelper iTestOutputHelper) : TestWithOutput(iTestOutputHelper)
{

	/// <summary>
	/// Valid sql query returns items
	/// </summary>
	[Fact]
	public async Task Valid_SqlQuery_ReturnsItems()
	{
		var queryResponse = await Client.SqlQueryAsync<Node>(new SqlQuery
		{
			Sql = "SELECT Description, Uri, InstanceType FROM Orion.Nodes ORDER BY Uri WITH ROWS 1 TO 3 WITH TOTALROWS"
		}, cancellationToken);
		queryResponse.Should().NotBeNull();
		queryResponse.Results.Should().NotBeEmpty();
	}

	/// <summary>
	/// Valid filtered query returns items
	/// </summary>
	[Fact]
	public async Task Valid_FilterQuery_ReturnsItems()
	{
		var queryResponse = await Client.FilterQueryAsync(new FilterQuery<Node>
		{
			OrderBy = nameof(Entity.Uri),
			Skip = 0,
			Take = 3,
		}, cancellationToken);
		queryResponse.Should().NotBeNull();
		queryResponse.Results.Should().NotBeEmpty();
	}
}
