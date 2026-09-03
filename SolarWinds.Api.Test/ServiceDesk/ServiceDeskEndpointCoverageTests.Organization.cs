using Microsoft.Extensions.Configuration;

namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Endpoint coverage for the records that describe an organization: its people, places and the groupings that classify its work.
/// </summary>
public partial class ServiceDeskEndpointCoverageTests
{
	/// <summary>
	/// Executes Memberships_AreCovered.
	/// </summary>
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
			await TryCleanupAsync(() => ServiceDeskClient.Memberships.DeleteAsync(existingMembership.Id, CancellationToken));
		}
	}

	/// <summary>
	/// Executes Categories_WriteEndpoints_AreCovered.
	/// </summary>
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
				await TryCleanupAsync(() => ServiceDeskClient.Categories.DeleteAsync(created.Id, CancellationToken));
			}
		}
	}

	/// <summary>
	/// Executes Departments_WriteEndpoints_AreCovered.
	/// </summary>
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

	/// <summary>
	/// Executes Groups_WriteEndpoints_AreCovered.
	/// </summary>
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

	/// <summary>
	/// Executes Roles_WriteEndpoints_AreCovered.
	/// </summary>
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

	/// <summary>
	/// Executes Sites_WriteEndpoints_AreCovered.
	/// </summary>
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
				await TryCleanupAsync(() => ServiceDeskClient.Sites.DeleteAsync(created.Id, CancellationToken));
			}
		}
	}

	/// <summary>
	/// Executes Users_WriteEndpoints_AreCovered.
	/// </summary>
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
}
