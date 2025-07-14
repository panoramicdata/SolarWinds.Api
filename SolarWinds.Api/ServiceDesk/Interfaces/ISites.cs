using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Sites API.
/// </summary>
public interface ISites
{
	/// <summary>
	/// Gets a list of sites.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of sites.</returns>
	[Get("/api/v2/sites.json")]
	Task<List<Site>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific site by ID.
	/// </summary>
	/// <param name="id">The ID of the site.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The site.</returns>
	[Get("/api/v2/sites/{id}.json")]
	Task<Site> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new site.
	/// </summary>
	/// <param name="site">The site to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created site.</returns>
	[Post("/api/v2/sites.json")]
	Task<Site> CreateAsync([Body] Site site, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing site.
	/// </summary>
	/// <param name="id">The ID of the site to update.</param>
	/// <param name="site">The site data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated site.</returns>
	[Put("/api/v2/sites/{id}.json")]
	Task<Site> UpdateAsync(int id, [Body] Site site, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a site.
	/// </summary>
	/// <param name="id">The ID of the site to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/sites/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}