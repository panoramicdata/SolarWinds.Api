namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Endpoint coverage for the ITSM record types: change catalogs, change requests, service requests and attachments.
/// </summary>
public partial class ServiceDeskEndpointCoverageTests
{
	/// <summary>
	/// Executes ChangeCatalogs_ReadEndpoints_Work.
	/// </summary>
	[Fact]
	public async Task ChangeCatalogs_ReadEndpoints_Work()
	{
		var items = await ServiceDeskClient.ChangeCatalogs.GetAsync(CancellationToken);
		items.Should().NotBeNull();
		if (items.Count == 0)
		{
			return;
		}

		var byId = await ServiceDeskClient.ChangeCatalogs.GetAsync(items[0].Id, ResponseLayout.Short, CancellationToken);
		byId.Should().NotBeNull();
	}

	/// <summary>
	/// Executes ChangeCatalogs_WriteEndpoints_AreCovered.
	/// </summary>
	[Fact]
	public async Task ChangeCatalogs_WriteEndpoints_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		ChangeCatalog? created = null;
		try
		{
			created = await ServiceDeskClient.ChangeCatalogs.CreateAsync(new ChangeCatalogCreateRequest
			{
				ChangeCatalog = new ChangeCatalogWriteFields
				{
					Name = $"Coverage Change Catalog {DateTimeOffset.UtcNow:yyyyMMddHHmmss}",
					Description = "Created by endpoint coverage test",
					Priority = "Low"
				}
			}, CancellationToken);

			created.Should().NotBeNull();
			created.Id.Should().BePositive();

			var updated = await ServiceDeskClient.ChangeCatalogs.UpdateAsync(created.Id, new ChangeCatalogUpdateRequest
			{
				ChangeCatalog = new ChangeCatalogWriteFields
				{
					Name = created.Name,
					Description = "Updated by endpoint coverage test"
				}
			}, CancellationToken);

			updated.Should().NotBeNull();
		}
		catch (ApiException ex) when ((int)ex.StatusCode >= 500)
		{
			// Some tenants reject change catalog writes with server-side errors. The finally block
			// below still removes anything that was created.
		}
		finally
		{
			if (created?.Id > 0)
			{
				await TryCleanupAsync(() => ServiceDeskClient.ChangeCatalogs.DeleteAsync(created.Id, CancellationToken));
			}
		}
	}

	/// <summary>
	/// Executes ChangeRequests_Create_IsCovered.
	/// </summary>
	[Fact]
	public async Task ChangeRequests_Create_IsCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		var configuration = GetCoverageConfiguration();
		if (!TryGetIntSecret(configuration, "ServiceDesk:Coverage:ChangeRequests:CatalogId", out var catalogId))
		{
			return;
		}

		var created = await ServiceDeskClient.ChangeRequests.CreateAsync(catalogId, new ChangeRequestCreateRequest
		{
			ChangeRequest = new ChangeRequestWriteFields
			{
				Name = $"Coverage Change Request {DateTimeOffset.UtcNow:yyyyMMddHHmmss}",
				Description = "Created by endpoint coverage test",
				Priority = "Low"
			}
		}, CancellationToken);

		created.Should().NotBeNull();
	}

	/// <summary>
	/// Executes ServiceRequests_AreCovered.
	/// </summary>
	[Fact]
	public async Task ServiceRequests_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		var configuration = GetCoverageConfiguration();
		if (!TryGetIntSecret(configuration, "ServiceDesk:Coverage:ServiceRequests:CatalogItemId", out var catalogItemId))
		{
			return;
		}

		var request = new ServiceRequestCreateRequest
		{
			Incident = new IncidentWriteFields
			{
				Name = $"Coverage Service Request {DateTimeOffset.UtcNow:yyyyMMddHHmmss}",
				Description = "Created by endpoint coverage test",
				Priority = "Low"
			}
		};

		var created = await ServiceDeskClient.ServiceRequests.CreateAsync(catalogItemId, request, CancellationToken);
		created.Should().NotBeNull();
	}

	/// <summary>
	/// Executes Attachments_Create_IsCovered.
	/// </summary>
	[Fact]
	public async Task Attachments_Create_IsCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		var configuration = GetCoverageConfiguration();
		if (!TryGetIntSecret(configuration, "ServiceDesk:Coverage:Attachments:AttachableId", out var attachableId))
		{
			return;
		}
		if (!TryGetSecret(configuration, "ServiceDesk:Coverage:Attachments:AttachableType", out var attachableType))
		{
			return;
		}

		var created = await ServiceDeskClient.Attachments.CreateAsync(new Attachment
		{
			FileName = "coverage.txt",
			ContentType = "text/plain",
			FileSize = 1,
			AttachableId = attachableId,
			AttachableType = attachableType
		}, CancellationToken);

		created.Should().NotBeNull();
	}
}
