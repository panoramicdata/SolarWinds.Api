using Refit;
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
	[Get("/changes.json")]
	public Task<List<Change>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific change by ID.
	/// </summary>
	/// <param name="id">The ID of the change.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The change.</returns>
	[Get("/changes/{id}.json")]
	public Task<Change> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new change.
	/// </summary>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created change.</returns>
	[Post("/changes.json")]
	public Task<Change> CreateAsync([Body] ChangeCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing change.
	/// </summary>
	/// <param name="id">The ID of the change to update.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated change.</returns>
	[Put("/changes/{id}.json")]
	public Task<Change> UpdateAsync(int id, [Body] ChangeUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a change.
	/// </summary>
	/// <param name="id">The ID of the change to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/changes/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}