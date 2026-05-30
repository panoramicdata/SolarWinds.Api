namespace SolarWinds.Api.Test.ServiceDesk;

public class IncidentQueryIntegrationTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetAll_WithLayoutLong_ReturnsItems()
	{
		var items = await QueryAsync(r => r.Layout = ResponseLayout.Long);
		items.Should().NotBeNull();
	}

	[Fact]
	public async Task GetAll_WithUpdatedSelector_ReturnsItems()
	{
		var items = await QueryAsync(r => r.Updated = "7");
		items.Should().NotBeNull();
	}

	[Fact]
	public async Task GetAll_WithUpdatedCustomRange_ReturnsItems()
	{
		var from = DateTime.UtcNow.Date.AddDays(-14);
		var to = DateTime.UtcNow.Date.AddDays(-7);

		var items = await QueryAsync(r =>
		{
			r.Updated = "Select Date Range";
			r.UpdatedCustomGte = from;
			r.UpdatedCustomLte = to;
		});

		items.Should().NotBeNull();
	}

	[Fact]
	public async Task GetAll_WithUpdatedFrom_ReturnsItems()
	{
		var items = await QueryAsync(r => r.UpdatedFrom = DateTime.UtcNow.Date.AddDays(-14));
		items.Should().NotBeNull();
	}

	[Fact]
	public async Task GetAll_WithUpdatedTo_ReturnsItems()
	{
		var from = DateTime.UtcNow.Date.AddDays(-14);
		var to = DateTime.UtcNow.Date.AddDays(-1);

		var items = await QueryAsync(r =>
		{
			r.UpdatedFrom = from;
			r.UpdatedTo = to;
		});
		items.Should().NotBeNull();
	}

	[Fact]
	public async Task GetAll_WithCreatedFrom_ReturnsItems()
	{
		var items = await QueryAsync(r => r.CreatedFrom = DateTime.UtcNow.Date.AddDays(-30));
		items.Should().NotBeNull();
	}

	[Fact]
	public async Task GetAll_WithCreatedTo_ReturnsItems()
	{
		var from = DateTime.UtcNow.Date.AddDays(-30);
		var to = DateTime.UtcNow.Date.AddDays(-1);

		var items = await QueryAsync(r =>
		{
			r.CreatedFrom = from;
			r.CreatedTo = to;
		});
		items.Should().NotBeNull();
	}

	[Fact]
	public async Task GetAll_WithPage_ReturnsItems()
	{
		var items = await QueryAsync(r => r.Page = 1);
		items.Should().NotBeNull();
	}

	[Fact]
	public async Task GetAll_WithPerPage_ReturnsItemsLimitedByPerPage()
	{
		const int perPage = 1;
		var items = await QueryAsync(r => r.PerPage = perPage);

		items.Should().NotBeNull();
		items.Count.Should().BeLessThanOrEqualTo(perPage);
	}

	[Fact]
	public async Task GetAll_WithAllSupportedParameters_ReturnsItems()
	{
		var from = DateTime.UtcNow.Date.AddDays(-14);
		var to = DateTime.UtcNow.Date.AddDays(-7);

		var items = await QueryAsync(r =>
		{
			r.Layout = ResponseLayout.Long;
			r.Updated = "Select Date Range";
			r.UpdatedCustomGte = from;
			r.UpdatedCustomLte = to;
			r.UpdatedFrom = from;
			r.UpdatedTo = to;
			r.CreatedFrom = from.AddDays(-14);
			r.CreatedTo = to;
			r.Page = 1;
			r.PerPage = 10;
		});

		items.Should().NotBeNull();
	}

	private async Task<List<Incident>> QueryAsync(Action<GetIncidentsRequest> configure)
	{
		var request = new GetIncidentsRequest
		{
			Layout = ResponseLayout.Short,
		};

		configure(request);

		return await ServiceDeskClient.Incidents.GetAsync(request, CancellationToken);
	}
}
