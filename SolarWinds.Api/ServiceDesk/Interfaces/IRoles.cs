using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Roles API.
/// </summary>
public interface IRoles
{
	/// <summary>
	/// Gets a list of roles.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of roles.</returns>
	[Get("/roles.json")]
	Task<List<Role>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific role by ID.
	/// </summary>
	/// <param name="id">The ID of the role.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The role.</returns>
	[Get("/roles/{id}.json")]
	Task<Role> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new role.
	/// </summary>
	/// <param name="role">The role to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created role.</returns>
	[Post("/roles.json")]
	Task<Role> CreateAsync([Body] Role role, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing role.
	/// </summary>
	/// <param name="id">The ID of the role to update.</param>
	/// <param name="role">The role data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated role.</returns>
	[Put("/roles/{id}.json")]
	Task<Role> UpdateAsync(int id, [Body] Role role, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a role.
	/// </summary>
	/// <param name="id">The ID of the role to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/roles/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}