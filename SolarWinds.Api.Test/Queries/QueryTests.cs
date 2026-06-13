using SolarWinds.Api.Orion;
using SolarWinds.Api.Queries;

namespace SolarWinds.Api.Test.Queries;

/// <summary>
/// Represents this type.
/// </summary>
public class QueryTests(ITestOutputHelper iTestOutputHelper) : TestWithOutput(iTestOutputHelper)
{
	/// <summary>
	/// Nodes custom properties query should include expected custom property fields
	/// </summary>
	[Fact]
	public void NodesCustomProperty_FilterQuery_Includes_dvcOwner_and_dvcTypeTier()
	{
		var query = new FilterQuery<NodesCustomProperty>
		{
			Skip = 0,
			Take = 1,
		}.GetSqlQuery();

		query.Sql.Should().Contain("cp_dvcOwner");
		query.Sql.Should().Contain("cp_dvcTypeTier");
	}

	/// <summary>
	/// Null query should throw an appropriate exception
	/// </summary>
	[Fact]
	public void Null_Throws_ArgumentNullException()
		=> ((Func<Task>)(async () => await OrionClient.SqlQueryAsync<Poller>(null!, CancellationToken.None))).Should().ThrowAsync<ArgumentNullException>();

	/// <summary>
	/// Query with null query should throw an appropriate exception
	/// </summary>
	[Fact]
	public void QueryNull_Throws_ArgumentException()
		=> ((Func<Task>)(async () => await OrionClient.SqlQueryAsync<Poller>(new SqlQuery(), CancellationToken.None))).Should().ThrowAsync<ArgumentException>();

	/// <summary>
	/// Valid SQL query returns items
	/// </summary>
	[Fact]
	public async Task JObject_SqlQuery_ReturnsItems()
	{
		var queryResponse = await OrionClient.SqlJObjectQueryAsync(new SqlQuery
		{
			Sql = "SELECT TOP 1 [Caption] FROM Orion.Nodes"
		}, CancellationToken.None);
		queryResponse.Should().NotBeNull();
		queryResponse.Results.Should().NotBeEmpty();
	}
}
