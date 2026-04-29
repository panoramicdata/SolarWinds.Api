using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Groups API.
/// </summary>
public interface IGroups
{
	/// <summary>
	/// Gets a list of groups.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of groups.</returns>
	[Get("/groups.json")]
	Task<List<Group>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific group by ID.
	/// </summary>
	/// <param name="id">The ID of the group.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The group.</returns>
	[Get("/groups/{id}.json")]
	Task<Group> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new group.
	/// </summary>
	/// <param name="group">The group to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created group.</returns>
	[Post("/groups.json")]
	Task<Group> CreateAsync([Body] Group group, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing group.
	/// </summary>
	/// <param name="id">The ID of the group to update.</param>
	/// <param name="group">The group data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated group.</returns>
	[Put("/groups/{id}.json")]
	Task<Group> UpdateAsync(int id, [Body] Group group, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a group.
	/// </summary>
	/// <param name="id">The ID of the group to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/groups/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}