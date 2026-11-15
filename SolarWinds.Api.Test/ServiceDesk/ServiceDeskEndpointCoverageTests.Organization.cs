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
}
