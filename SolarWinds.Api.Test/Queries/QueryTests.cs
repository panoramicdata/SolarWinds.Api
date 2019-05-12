using SolarWinds.Api.Orion;
using SolarWinds.Api.Queries;
using System;
using Xunit;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test
{
	public class QueryTests : TestWithOutput
	{
		public QueryTests(ITestOutputHelper iTestOutputHelper) : base(iTestOutputHelper)
		{
		}

		/// <summary>
		/// Null query should throw an appropriate exception
		/// </summary>
		[Fact]
		public void Null_Throws_ArgumentNullException()
			=> Assert.ThrowsAsync<ArgumentNullException>(async () => await Client.SqlQueryAsync<Poller>(null).ConfigureAwait(false));

		/// <summary>
		/// Query with null query should throw an appropriate exception
		/// </summary>
		[Fact]
		public void QueryNull_Throws_ArgumentException()
			=> Assert.ThrowsAsync<ArgumentException>(async () => await Client.SqlQueryAsync<Poller>(new SqlQuery()).ConfigureAwait(false));

		/// <summary>
		/// Valid SQL query returns items
		/// </summary>
		[Fact]
		public async void JObject_SqlQuery_ReturnsItems()
		{
			var queryResponse = await Client.SqlJObjectQueryAsync(new SqlQuery
			{
				Sql = "SELECT TOP 1 [Caption] FROM Orion.Nodes"
			}).ConfigureAwait(false);
			Assert.NotNull(queryResponse);
			Assert.NotEmpty(queryResponse.Results);
		}
	}
}
