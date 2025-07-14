using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
	[Get("/api/v2/releases.json")]
	Task<List<Release>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific release by ID.
	/// </summary>
	/// <param name="id">The ID of the release.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The release.</returns>
	[Get("/api/v2/releases/{id}.json")]
	Task<Release> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new release.
	/// </summary>
	/// <param name="release">The release to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created release.</returns>
	[Post("/api/v2/releases.json")]
	Task<Release> CreateAsync([Body] Release release, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing release.
	/// </summary>
	/// <param name="id">The ID of the release to update.</param>
	/// <param name="release">The release data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated release.</returns>
	[Put("/api/v2/releases/{id}.json")]
	Task<Release> UpdateAsync(int id, [Body] Release release, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a release.
	/// </summary>
	/// <param name="id">The ID of the release to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/releases/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}