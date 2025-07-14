using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Changes API.
/// </summary>
public interface IChanges
{
	/// <summary>
	/// Gets a list of changes.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of changes.</returns>
	[Get("/api/v2/changes.json")]
	Task<List<Change>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific change by ID.
	/// </summary>
	/// <param name="id">The ID of the change.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The change.</returns>
	[Get("/api/v2/changes/{id}.json")]
	Task<Change> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new change.
	/// </summary>
	/// <param name="change">The change to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created change.</returns>
	[Post("/api/v2/changes.json")]
	Task<Change> CreateAsync([Body] Change change, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing change.
	/// </summary>
	/// <param name="id">The ID of the change to update.</param>
	/// <param name="change">The change data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated change.</returns>
	[Put("/api/v2/changes/{id}.json")]
	Task<Change> UpdateAsync(int id, [Body] Change change, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a change.
	/// </summary>
	/// <param name="id">The ID of the change to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/changes/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}