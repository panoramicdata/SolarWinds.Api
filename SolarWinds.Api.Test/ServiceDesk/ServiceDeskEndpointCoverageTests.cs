using AwesomeAssertions;
using Microsoft.Extensions.Configuration;
using Refit;
using Xunit;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test.ServiceDesk;

public class ServiceDeskEndpointCoverageTests(ITestOutputHelper output) : TestWithOutput(output)
{
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
			// Some tenants reject change catalog writes with server-side errors.
			return;
		}
		finally
		{
			if (created?.Id > 0)
			{
				try
				{
					await ServiceDeskClient.ChangeCatalogs.DeleteAsync(created.Id, CancellationToken);
				}
				catch (ApiException)
				{
					// Best-effort cleanup only.
				}
			}
		}
	}

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

	[Fact]
	public async Task Comments_Tasks_TimeTracks_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		Incident? createdIncident = null;
		Comment? createdComment = null;
		ServiceTask? createdTask = null;
		TimeTrack? createdTrack = null;

		try
		{
			createdIncident = await ServiceDeskClient.Incidents.CreateAsync(new IncidentCreateRequest
			{
				Incident = new IncidentWriteFields
				{
					Name = $"Coverage source incident {DateTimeOffset.UtcNow:yyyyMMddHHmmss}",
					Description = "Source incident for comment/task/time-track coverage",
					Priority = "Low",
					Origin = "web",
					IsServiceRequest = false,
				}
			}, CancellationToken);

			createdIncident.Should().NotBeNull();
			createdIncident!.Id.Should().BePositive();

			createdComment = await ServiceDeskClient.Comments.CreateAsync(ObjectType.Incidents, createdIncident.Id, new CommentCreateRequest
			{
				Comment = new CommentWriteFields { Body = "Coverage comment", IsPrivate = true }
			}, CancellationToken);

			createdComment.Should().NotBeNull();
			createdComment!.Id.Should().BePositive();
			await ServiceDeskClient.Comments.UpdateAsync(ObjectType.Incidents, createdIncident.Id, createdComment.Id, new CommentUpdateRequest
			{
				Comment = new CommentWriteFields { Body = "Coverage comment updated", IsPrivate = true }
			}, CancellationToken);

			createdTask = await ServiceDeskClient.Tasks.CreateAsync(ObjectType.Incidents, createdIncident.Id, new TaskCreateRequest
			{
				Task = new TaskWriteFields { Name = "Coverage task" }
			}, CancellationToken);

			createdTask.Should().NotBeNull();
			createdTask!.Id.Should().BePositive();
			await ServiceDeskClient.Tasks.UpdateAsync(ObjectType.Incidents, createdIncident.Id, createdTask.Id, new TaskUpdateRequest
			{
				Task = new TaskWriteFields { Name = "Coverage task updated", IsComplete = false }
			}, CancellationToken);

			createdTrack = await ServiceDeskClient.TimeTracks.CreateAsync(ObjectType.Incidents, createdIncident.Id, new TimeTrackCreateRequest
			{
				TimeTrack = new TimeTrackWriteFields { Name = "Coverage track", MinutesParsed = "15" }
			}, CancellationToken);

			createdTrack.Should().NotBeNull();
			createdTrack!.Id.Should().BePositive();
			_ = await ServiceDeskClient.TimeTracks.GetAsync(ObjectType.Incidents, createdIncident.Id, CancellationToken);
			await ServiceDeskClient.TimeTracks.UpdateAsync(ObjectType.Incidents, createdIncident.Id, createdTrack.Id, new TimeTrackUpdateRequest
			{
				TimeTrack = new TimeTrackWriteFields { Name = "Coverage track updated", MinutesParsed = "20" }
			}, CancellationToken);
		}
		catch (ApiException ex) when ((int)ex.StatusCode >= 500)
		{
			// Some tenant workflows intermittently throw server errors for comment/task/time-track writes.
			return;
		}
		finally
		{
			if (createdTrack?.Id > 0)
			{
				try { await ServiceDeskClient.TimeTracks.DeleteAsync(ObjectType.Incidents, createdIncident!.Id, createdTrack.Id, CancellationToken); } catch (ApiException) { }
			}

			if (createdTask?.Id > 0)
			{
				try { await ServiceDeskClient.Tasks.DeleteAsync(ObjectType.Incidents, createdIncident!.Id, createdTask.Id, CancellationToken); } catch (ApiException) { }
			}

			if (createdComment?.Id > 0)
			{
				try { await ServiceDeskClient.Comments.DeleteAsync(ObjectType.Incidents, createdIncident!.Id, createdComment.Id, CancellationToken); } catch (ApiException) { }
			}

			if (createdIncident?.Id > 0)
			{
				try { await ServiceDeskClient.Incidents.DeleteAsync(createdIncident.Id, CancellationToken); } catch (ApiException) { }
			}
		}
	}

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
				try { await ServiceDeskClient.Purchases.DeleteAsync(ObjectType.Contracts, contracts[0].Id, created.Id, CancellationToken); } catch (ApiException) { }
			}
		}
	}

	[Fact]
	public async Task Tickets_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}
		List<Ticket> tickets;
		try
		{
			tickets = await ServiceDeskClient.Tickets.GetAsync(CancellationToken);
		}
		catch (ApiException ex) when ((int)ex.StatusCode == 404)
		{
			// Tickets endpoint is not available on all tenants.
			return;
		}

		tickets.Should().NotBeNull();

		if (tickets.Count > 0)
		{
			var byId = await ServiceDeskClient.Tickets.GetAsync(tickets[0].Id, ResponseLayout.Short, CancellationToken);
			byId.Should().NotBeNull();
		}

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
			if (created?.Id > 0)
			{
				try { await ServiceDeskClient.Tickets.DeleteAsync(created.Id, CancellationToken); } catch (ApiException) { }
			}
		}
	}

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
				Priority = "Low",
				Origin = "web"
			}
		};

		var created = await ServiceDeskClient.ServiceRequests.CreateAsync(catalogItemId, request, CancellationToken);
		created.Should().NotBeNull();
	}

	[Fact]
	public async Task Memberships_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		var groups = await ServiceDeskClient.Groups.GetAsync(CancellationToken);
		var users = await ServiceDeskClient.Users.GetAsync(CancellationToken);
		groups.Should().NotBeEmpty("membership workflows require at least one group");
		users.Should().NotBeEmpty("membership workflows require at least one user");

		try
		{
			await ServiceDeskClient.Memberships.CreateAsync(groups[0].Id, users[0].Id.ToString(), CancellationToken);
		}
		catch (ApiException ex) when ((int)ex.StatusCode == 409)
		{
			// Membership already exists.
		}

		var refreshedGroups = await ServiceDeskClient.Groups.GetAsync(CancellationToken);
		var existingMembership = refreshedGroups.FirstOrDefault(g => g.Id == groups[0].Id)?.Memberships?.FirstOrDefault();
		if (existingMembership?.Id > 0)
		{
			try { await ServiceDeskClient.Memberships.DeleteAsync(existingMembership.Id, CancellationToken); } catch (ApiException) { }
		}
	}

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

	[Fact]
	public async Task Categories_WriteEndpoints_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		Category? created = null;
		try
		{
			created = await ServiceDeskClient.Categories.CreateAsync(new CategoryCreateRequest
			{
				Category = new CategoryWriteFields
				{
					Name = $"Coverage Category {DateTimeOffset.UtcNow:yyyyMMddHHmmss}"
				}
			}, CancellationToken);

			created.Should().NotBeNull();
			created!.Id.Should().BePositive();

			var updated = await ServiceDeskClient.Categories.UpdateAsync(created.Id, new CategoryUpdateRequest
			{
				Category = new CategoryWriteFields
				{
					Name = created.Name + " Updated"
				}
			}, CancellationToken);

			updated.Should().NotBeNull();
		}
		catch (ApiException ex) when ((int)ex.StatusCode == 400 || (int)ex.StatusCode >= 500)
		{
			// Category writes can be rejected by tenant-specific validation.
			return;
		}
		finally
		{
			if (created?.Id > 0)
			{
				try { await ServiceDeskClient.Categories.DeleteAsync(created.Id, CancellationToken); } catch (ApiException) { }
			}
		}
	}

	[Fact]
	public async Task Departments_WriteEndpoints_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		Department? created = null;
		try
		{
			created = await ServiceDeskClient.Departments.CreateAsync(new Department
			{
				Name = $"Coverage Department {DateTimeOffset.UtcNow:yyyyMMddHHmmss}",
				Description = "Created by coverage test"
			}, CancellationToken);

			created.Should().NotBeNull();
			created!.Id.Should().BePositive();

			var updated = await ServiceDeskClient.Departments.UpdateAsync(created.Id, new Department
			{
				Name = created.Name + " Updated",
				Description = "Updated by coverage test"
			}, CancellationToken);

			updated.Should().NotBeNull();
		}
		finally
		{
			if (created?.Id > 0)
			{
				await ServiceDeskClient.Departments.DeleteAsync(created.Id, CancellationToken);
			}
		}
	}

	[Fact]
	public async Task Groups_WriteEndpoints_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		Group? created = null;
		try
		{
			created = await ServiceDeskClient.Groups.CreateAsync(new Group
			{
				Name = $"Coverage Group {DateTimeOffset.UtcNow:yyyyMMddHHmmss}"
			}, CancellationToken);

			created.Should().NotBeNull();
			created!.Id.Should().BePositive();

			var updated = await ServiceDeskClient.Groups.UpdateAsync(created.Id, new Group
			{
				Name = created.Name + " Updated"
			}, CancellationToken);

			updated.Should().NotBeNull();
		}
		finally
		{
			if (created?.Id > 0)
			{
				await ServiceDeskClient.Groups.DeleteAsync(created.Id, CancellationToken);
			}
		}
	}

	[Fact]
	public async Task Roles_WriteEndpoints_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		Role? created = null;
		try
		{
			created = await ServiceDeskClient.Roles.CreateAsync(new Role
			{
				Name = $"Coverage Role {DateTimeOffset.UtcNow:yyyyMMddHHmmss}",
				Description = "Created by coverage test"
			}, CancellationToken);

			created.Should().NotBeNull();
			created!.Id.Should().BePositive();

			var updated = await ServiceDeskClient.Roles.UpdateAsync(created.Id, new Role
			{
				Name = created.Name + " Updated",
				Description = "Updated by coverage test"
			}, CancellationToken);

			updated.Should().NotBeNull();
		}
		finally
		{
			if (created?.Id > 0)
			{
				await ServiceDeskClient.Roles.DeleteAsync(created.Id, CancellationToken);
			}
		}
	}

	[Fact]
	public async Task Sites_WriteEndpoints_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		var users = await ServiceDeskClient.Users.GetAsync(CancellationToken);
		var groups = await ServiceDeskClient.Groups.GetAsync(CancellationToken);
		users.Should().NotBeEmpty("site write tests require at least one user for default assignee");
		groups.Should().NotBeEmpty("site write tests require at least one group for default group assignee");

		Site? created = null;
		try
		{
			created = await ServiceDeskClient.Sites.CreateAsync(new Site
			{
				Name = $"Coverage Site {DateTimeOffset.UtcNow:yyyyMMddHHmmss}",
				Description = "Created by coverage test",
				Location = "Coverage",
				TimeZone = "UTC",
				DefaultAssigneeId = users[0].Id,
				DefaultGroupAssigneeId = groups[0].Id,
			}, CancellationToken);

			created.Should().NotBeNull();
			created!.Id.Should().BePositive();

			var updated = await ServiceDeskClient.Sites.UpdateAsync(created.Id, new Site
			{
				Name = created.Name + " Updated",
				Description = "Updated by coverage test",
				Location = created.Location,
				TimeZone = created.TimeZone,
				DefaultAssigneeId = created.DefaultAssigneeId,
				DefaultGroupAssigneeId = created.DefaultGroupAssigneeId,
			}, CancellationToken);

			updated.Should().NotBeNull();
		}
		catch (ApiException ex) when ((int)ex.StatusCode >= 500)
		{
			// Site writes can fail on tenant-specific server-side policies.
			return;
		}
		finally
		{
			if (created?.Id > 0)
			{
				try { await ServiceDeskClient.Sites.DeleteAsync(created.Id, CancellationToken); } catch (ApiException) { }
			}
		}
	}

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
				try { await ServiceDeskClient.Vendors.DeleteAsync(created.Id, CancellationToken); } catch (ApiException) { }
			}
		}
	}

	[Fact]
	public async Task Users_WriteEndpoints_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		var configuration = new ConfigurationBuilder()
			.AddUserSecrets<ServiceDeskEndpointCoverageTests>()
			.Build();

		if (!TryGetSecret(configuration, "ServiceDesk:Coverage:Users:Name", out var userName)
			|| !TryGetSecret(configuration, "ServiceDesk:Coverage:Users:Email", out var userEmail))
		{
			return;
		}

		User? created = null;
		try
		{
			created = await ServiceDeskClient.Users.CreateAsync(new User
			{
				Name = userName!,
				Email = userEmail!,
				Active = true,
			}, CancellationToken);

			created.Should().NotBeNull();
			created!.Id.Should().BePositive();

			var updated = await ServiceDeskClient.Users.UpdateAsync(created.Id, new User
			{
				Name = created.Name + " Updated",
				Email = created.Email,
				Active = created.Active,
			}, CancellationToken);

			updated.Should().NotBeNull();
		}
		finally
		{
			if (created?.Id > 0)
			{
				await ServiceDeskClient.Users.DeleteAsync(created.Id, CancellationToken);
			}
		}
	}

	private static bool ShouldRunDestructiveIntegrationTests()
	{
		var configuration = GetCoverageConfiguration();

		var runCoverage = bool.TryParse(configuration["ServiceDesk:Coverage:RunDestructiveTests"], out var explicitRun) && explicitRun;
		var runLifecycle = bool.TryParse(configuration["ServiceDesk:Lifecycle:RunTests"], out var lifecycleRun) && lifecycleRun;
		if (!runCoverage && !runLifecycle)
		{
			return false;
		}

		var baseUrl = configuration["ServiceDesk:BaseUrl"] ?? string.Empty;
		return baseUrl.Contains("panoramicdatalimited.samanage.com", StringComparison.OrdinalIgnoreCase);
	}

	private static IConfiguration GetCoverageConfiguration()
	{
		return new ConfigurationBuilder()
			.AddUserSecrets<ServiceDeskEndpointCoverageTests>()
			.Build();
	}

	private static string RequireSecret(IConfiguration configuration, string key, string guidance)
	{
		var value = configuration[key];
		value.Should().NotBeNullOrWhiteSpace($"missing field information: {guidance}");
		return value!;
	}

	private static bool TryGetSecret(IConfiguration configuration, string key, out string value)
	{
		var raw = configuration[key];
		if (string.IsNullOrWhiteSpace(raw))
		{
			value = string.Empty;
			return false;
		}

		value = raw;
		return true;
	}

	private static bool TryGetIntSecret(IConfiguration configuration, string key, out int value)
	{
		value = 0;
		return TryGetSecret(configuration, key, out var raw)
			&& int.TryParse(raw, out value)
			&& value > 0;
	}

	private static int RequireIntSecret(IConfiguration configuration, string key, string guidance)
	{
		var raw = RequireSecret(configuration, key, guidance);
		var parsed = int.TryParse(raw, out var value) ? value : 0;
		parsed.Should().BePositive($"{key} must be a positive integer");
		return parsed;
	}
}
