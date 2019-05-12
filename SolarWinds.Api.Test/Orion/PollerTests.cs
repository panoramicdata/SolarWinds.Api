using SolarWinds.Api.Orion;
using SolarWinds.Api.Queries;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test.Orion
{
	public class PollerTests : TestWithOutput
	{
		public PollerTests(ITestOutputHelper iTestOutputHelper) : base(iTestOutputHelper)
		{
		}

		/// <summary>
		/// Valid sql query returns items
		/// </summary>
		[Fact]
		public async void Valid_SqlQuery_ReturnsItems()
		{
			var queryResponse = await Client.SqlQueryAsync<Poller>(new SqlQuery
			{
				Sql = "SELECT Description, Uri, InstanceType, Enabled, PollerType FROM Orion.Pollers WHERE PollerID>@p ORDER BY PollerID WITH ROWS 1 TO 3 WITH TOTALROWS",
				Parameters = new Dictionary<string, object>
				{
					{ "p", 0 }
				}
			}).ConfigureAwait(false);
			Assert.NotNull(queryResponse);
			Assert.NotEmpty(queryResponse.Results);
		}

		/// <summary>
		/// Valid filtered query returns items
		/// </summary>
		[Fact]
		public async void Valid_FilterQuery_ReturnsItems()
		{
			var queryResponse = await Client.FilterQueryAsync(new FilterQuery<Poller>
			{
				OrderBy = nameof(Entity.Uri),
				Skip = 0,
				Take = 3,
			}).ConfigureAwait(false);
			Assert.NotNull(queryResponse);
			Assert.NotEmpty(queryResponse.Results);
		}
	}
}
