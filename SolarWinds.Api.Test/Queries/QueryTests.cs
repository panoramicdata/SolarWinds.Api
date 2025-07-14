using System;
using System.Threading.Tasks;
using AwesomeAssertions;
using SolarWinds.Api.Orion;
using SolarWinds.Api.Queries;
using Xunit;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test.Queries;

public class QueryTests(ITestOutputHelper iTestOutputHelper) : TestWithOutput(iTestOutputHelper)
{

	/// <summary>
	/// Null query should throw an appropriate exception
	/// </summary>
	[Fact]
	public void Null_Throws_ArgumentNullException()
		=> ((Func<Task>)(async () => await Client.SqlQueryAsync<Poller>(null))).Should().ThrowAsync<ArgumentNullException>();

	/// <summary>
	/// Query with null query should throw an appropriate exception
	/// </summary>
	[Fact]
	public void QueryNull_Throws_ArgumentException()
		=> ((Func<Task>)(async () => await Client.SqlQueryAsync<Poller>(new SqlQuery()))).Should().ThrowAsync<ArgumentException>();

	/// <summary>
	/// Valid SQL query returns items
	/// </summary>
	[Fact]
	public async Task JObject_SqlQuery_ReturnsItems()
	{
		var queryResponse = await Client.SqlJObjectQueryAsync(new SqlQuery
		{
			Sql = "SELECT TOP 1 [Caption] FROM Orion.Nodes"
		});
		queryResponse.Should().NotBeNull();
		queryResponse.Results.Should().NotBeEmpty();
	}
}
