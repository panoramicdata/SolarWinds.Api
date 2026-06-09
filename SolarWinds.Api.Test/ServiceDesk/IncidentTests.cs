using System.Diagnostics;
using Newtonsoft.Json;

namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class IncidentTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Executes GetAll_ReturnsItems.
	/// </summary>
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

	/// <summary>
	/// Get Incidents with Title filter, and a wide range of State filters to ensure we get results, since Title is not unique.
	/// </summary>
	[Fact]
	public async Task GetAll_TitleFilter_ReturnsItems()
	{

		var getIncidentsRequest = new GetIncidentsRequest
		{
			Title = ["New Laptop Request"],
			State = [
				"New",
				"Pending Assignment",
				"Interrupted",
				"Pending Closure_Comp Team",
				"Closed Non-Response",
				"SEC: WIP",
				"Pre-Vendor",
				"TPRM: Waiting on Feedback",
				"Review Documentation",
				"Researching",
				"Waiting on Equipment",
				"Quarantine Pending Test",
				"Quarantine",
				"Assigned",
				"Comment Added",
				"Work in Progress",
				"Waiting on Approval",
				"Waiting on Colleague",
				"Waiting on Vendor",
				"Escalated to Admin",
				"Escalated to Clinical",
				"On Hold",
				"Scheduled",
				"Approved",
				"Escalated - Billing HH",
				"Escalated - Billing HOS",
				"Build in Progress",
				"Project",
				"SEC: Waiting on Feedback",
				"Interim Resolution"
			]
		};

		// Get all incidents that match
		var items = await ServiceDeskClient
				.Incidents
				.GetAsync(getIncidentsRequest, CancellationToken);

		items.Should().NotBeNullOrEmpty();
		items.Should().OnlyHaveUniqueItems(i => i.Id);
		items.Should().OnlyHaveUniqueItems(i => i.Number);
		items.Count.Should().BeLessThanOrEqualTo(10);
	}

	/// <summary>
	/// Executes GetById_WithValidId_ReturnsItem.
	/// </summary>
	[Fact]
	public async Task GetById_WithValidId_ReturnsItem()
	{
		var item = await GetResolvableIncidentAsync();
		item.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetEntityGeneralInfo_WithValidId_ReturnsStates.
	/// </summary>
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

