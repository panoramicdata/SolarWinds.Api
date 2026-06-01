namespace SolarWinds.Api.Test.ServiceDesk;

public class IncidentTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetAll_ReturnsItems()
	{
		var items = await ServiceDeskClient.Incidents.GetAsync(new GetIncidentsRequest
		{
			Page = 1,
			PerPage = 10
		}, CancellationToken);

		items.Should().NotBeNullOrEmpty();
		items.Should().OnlyHaveUniqueItems(i => i.Id);
		items.Should().OnlyHaveUniqueItems(i => i.Number);
		items.Count.Should().BeLessThanOrEqualTo(10);
	}

	[Fact]
	public async Task GetById_WithValidId_ReturnsItem()
	{
		var item = await GetResolvableIncidentAsync();
		item.Should().NotBeNull();
	}

	[Fact]
	public async Task GetEntityGeneralInfo_WithValidId_ReturnsStates()
	{
		var item = await GetResolvableIncidentAsync();

		IncidentEntityGeneralInfo? info = null;
		ApiException? lastServerException = null;
		for (var attempt = 1; attempt <= 3; attempt++)
		{
			try
			{
				info = await ServiceDeskClient.Incidents.GetEntityGeneralInfoAsync(item.Id, CancellationToken);
				break;
			}
			catch (ApiException ex) when ((int)ex.StatusCode >= 500)
			{
				lastServerException = ex;
				if (attempt < 3)
				{
					await Task.Delay(TimeSpan.FromSeconds(1), CancellationToken);
				}
			}
		}

		if (info is null && lastServerException is not null)
		{
			throw lastServerException;
		}

		info.Should().NotBeNull();
		info.States.Should().NotBeNull();
		info.States.Should().NotBeEmpty();
		info.States.Should().Contain(state => state.Id > 0);
		info.States.Should().Contain(state => state.Selected);
	}

	private async Task<Incident> GetResolvableIncidentAsync()
	{
		var items = await ServiceDeskClient.Incidents.GetAsync(new GetIncidentsRequest
		{
			Page = 1,
			PerPage = 10
		}, CancellationToken);

		items.Should().NotBeNullOrEmpty();
		items.Should().OnlyHaveUniqueItems(i => i.Id);
		items.Should().OnlyHaveUniqueItems(i => i.Number);
		items.Count.Should().BeLessThanOrEqualTo(10);

		foreach (var candidate in items)
		{
			try
			{
				return await ServiceDeskClient.Incidents.GetAsync(candidate.Id, ResponseLayout.Short, CancellationToken);
			}
			catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
			{
				// Some list snapshots can include items deleted before follow-up GET by ID.
			}
		}

		throw new Xunit.Sdk.XunitException("No listed incident IDs could be resolved via GET by ID.");
	}
}

