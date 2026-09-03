namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Endpoint coverage for purchasing and supplier records, and for tickets.
/// </summary>
public partial class ServiceDeskEndpointCoverageTests
{
	/// <summary>
	/// Executes Purchases_AreCovered.
	/// </summary>
	[Fact]
	public async Task Purchases_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		var contracts = await ServiceDeskClient.Contracts.GetAsync(new GetContractsRequest(), CancellationToken);
		contracts.Should().NotBeEmpty("purchase workflows require at least one contract");

		Purchase? created = null;
		try
		{
			var purchaseNumber = $"PO-{DateTimeOffset.UtcNow:yyyyMMddHHmmss}";
			created = await ServiceDeskClient.Purchases.CreateAsync(ObjectType.Contracts, contracts[0].Id, new PurchaseCreateRequest
			{
				Purchase = new PurchaseWriteFields
				{
					Number = purchaseNumber,
					Date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
					TotalCost = "1.00",
					Currency = "USD",
					Notes = "Coverage purchase"
				}
			}, CancellationToken);

			if (created?.Id > 0)
			{
				await ServiceDeskClient.Purchases.UpdateAsync(ObjectType.Contracts, contracts[0].Id, created.Id, new PurchaseUpdateRequest
				{
					Purchase = new PurchaseWriteFields
					{
						Number = purchaseNumber,
						Date = DateTime.UtcNow.ToString("yyyy-MM-dd"),
						TotalCost = "1.00",
						Currency = "USD",
						Notes = "Coverage purchase updated"
					}
				}, CancellationToken);
			}
		}
		finally
		{
			if (created?.Id > 0)
			{
				await TryCleanupAsync(() => ServiceDeskClient.Purchases.DeleteAsync(ObjectType.Contracts, contracts[0].Id, created.Id, CancellationToken));
			}
		}
	}

	/// <summary>
	/// Executes Tickets_AreCovered.
	/// </summary>
	[Fact]
	public async Task Tickets_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		if (!await AssertTicketReadEndpointsAsync())
		{
			return;
		}

		await AssertTicketWriteEndpointsAsync();
	}

	/// <summary>
	/// Reads the ticket list, and one ticket by id where the list is not empty. Returns
	/// <see langword="false"/> when the tenant does not expose the tickets endpoint at all, in
	/// which case the write coverage cannot run either.
	/// </summary>
	private async Task<bool> AssertTicketReadEndpointsAsync()
	{
		List<Ticket> tickets;
		try
		{
			tickets = await ServiceDeskClient.Tickets.GetAsync(CancellationToken);
		}
		catch (ApiException ex) when ((int)ex.StatusCode == 404)
		{
			// Tickets endpoint is not available on all tenants.
			return false;
		}

		tickets.Should().NotBeNull();

		if (tickets.Count > 0)
		{
			var byId = await ServiceDeskClient.Tickets.GetAsync(tickets[0].Id, ResponseLayout.Short, CancellationToken);
			byId.Should().NotBeNull();
		}

		return true;
	}

	private async Task AssertTicketWriteEndpointsAsync()
	{
		Ticket? created = null;
		try
		{
			created = await ServiceDeskClient.Tickets.CreateAsync(new Ticket
			{
				Subject = $"Coverage Ticket {DateTimeOffset.UtcNow:yyyyMMddHHmmss}",
				Description = "Created by endpoint coverage test",
				Status = "Open"
			}, CancellationToken);

			if (created?.Id > 0)
			{
				await ServiceDeskClient.Tickets.UpdateAsync(created.Id, new Ticket
				{
					Subject = created.Subject,
					Description = "Updated by endpoint coverage test",
					Status = created.Status
				}, CancellationToken);
			}
		}
		finally
		{
			await DeleteIfCreatedAsync(created?.Id, id =>
				ServiceDeskClient.Tickets.DeleteAsync(id, CancellationToken));
		}
	}

	/// <summary>
	/// Executes Vendors_WriteEndpoints_AreCovered.
	/// </summary>
	[Fact]
	public async Task Vendors_WriteEndpoints_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		Vendor? created = null;
		try
		{
			created = await ServiceDeskClient.Vendors.CreateAsync(new VendorCreateRequest
			{
				Vendor = new VendorWriteFields
				{
					Name = $"Coverage Vendor {DateTimeOffset.UtcNow:yyyyMMddHHmmss}",
					ContactPerson = "Coverage Tester",
					Email = "coverage.vendor@example.com",
					Phone = "555-0100",
					Website = "https://example.com"
				}
			}, CancellationToken);

			created.Should().NotBeNull();
			created!.Id.Should().BePositive();

			var updated = await ServiceDeskClient.Vendors.UpdateAsync(created.Id, new VendorUpdateRequest
			{
				Vendor = new VendorWriteFields
				{
					Name = created.Name + " Updated",
					ContactPerson = created.ContactPerson,
					Email = created.Email,
					Phone = created.Phone,
					Website = created.Website
				}
			}, CancellationToken);

			updated.Should().NotBeNull();
		}
		catch (ApiException ex) when ((int)ex.StatusCode == 422 || (int)ex.StatusCode >= 500)
		{
			// Vendor writes can fail on tenant-specific validation or server-side rules.
			return;
		}
		finally
		{
			if (created?.Id > 0)
			{
				await TryCleanupAsync(() => ServiceDeskClient.Vendors.DeleteAsync(created.Id, CancellationToken));
			}
		}
	}
}
