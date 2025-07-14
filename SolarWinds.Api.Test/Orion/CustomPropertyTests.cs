using SolarWinds.Api.Orion;
using SolarWinds.Api.Queries;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using AwesomeAssertions;

namespace SolarWinds.Api.Test.Orion;

public class CustomPropertyTests(ITestOutputHelper iTestOutputHelper) : TestWithOutput(iTestOutputHelper)
{

	/// <summary>
	/// Valid sql query returns items
	/// </summary>
	[Fact]
	public async Task Valid_SqlQuery_ReturnsItems()
	{
		var queryResponse = await Client.SqlQueryAsync<CustomProperty>(new SqlQuery
		{
			Sql = "SELECT Table, Description, Field, DataType, Default, DisplayName FROM Orion.CustomProperty ORDER BY Uri WITH ROWS 1 TO 3 WITH TOTALROWS",
		});
		queryResponse.Should().NotBeNull();
		queryResponse.Results.Should().NotBeEmpty();
	}

	/// <summary>
	/// Valid filtered query returns items
	/// </summary>
	[Fact]
	public async Task Valid_FilterQuery_ReturnsItems()
	{
		var queryResponse = await Client.FilterQueryAsync(new FilterQuery<CustomProperty>
		{
			OrderBy = nameof(Entity.Uri),
			Skip = 0,
			Take = 3,
		});
		queryResponse.Should().NotBeNull();
		queryResponse.Results.Should().NotBeEmpty();
	}
}
