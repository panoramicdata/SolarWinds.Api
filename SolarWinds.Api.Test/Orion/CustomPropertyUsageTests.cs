using System.Threading.Tasks;
using AwesomeAssertions;
using SolarWinds.Api.Orion;
using SolarWinds.Api.Queries;
using Xunit;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test.Orion;

public class CustomPropertyUsageTests(ITestOutputHelper iTestOutputHelper) : TestWithOutput(iTestOutputHelper)
{

	/// <summary>
	/// Valid sql query returns items
	/// </summary>
	[Fact]
	public async Task Valid_SqlQuery_ReturnsItems()
	{
		var queryResponse = await Client.SqlQueryAsync<CustomPropertyUsage>(new SqlQuery
		{
			Sql = "SELECT DisplayName, Uri FROM Orion.CustomPropertyUsage ORDER BY Uri WITH ROWS 1 TO 3 WITH TOTALROWS"
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
		var queryResponse = await Client.FilterQueryAsync(new FilterQuery<CustomPropertyUsage>
		{
			OrderBy = nameof(Entity.Uri),
			Skip = 0,
			Take = 3,
		}, CancellationToken);
		queryResponse.Should().NotBeNull();
		queryResponse.Results.Should().NotBeEmpty();
	}
}
