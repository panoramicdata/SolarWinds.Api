using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Releases API.
/// </summary>
public interface IReleases
{
	/// <summary>
	/// Gets a list of releases.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of releases.</returns>
	[Get("/releases.json")]
	public Task<List<Release>> GetAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific release by ID.
	/// </summary>
	/// <param name="id">The ID of the release.</param>
	/// <param name="layout">The response layout.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The release.</returns>
	[Get("/releases/{id}.json")]
	public Task<Release> GetAsync(int id, [AliasAs("layout")] ResponseLayout layout, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new release.
	/// </summary>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created release.</returns>
	[Post("/releases.json")]
	public Task<Release> CreateAsync([Body] ReleaseCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing release.
	/// </summary>
	/// <param name="id">The ID of the release to update.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated release.</returns>
	[Put("/releases/{id}.json")]
	public Task<Release> UpdateAsync(int id, [Body] ReleaseUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a release.
	/// </summary>
	/// <param name="id">The ID of the release to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/releases/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}