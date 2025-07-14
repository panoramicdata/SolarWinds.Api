using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Catalog Items API.
/// </summary>
public interface ICatalogItems
{
	/// <summary>
	/// Gets a list of catalog items.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of catalog items.</returns>
	[Get("/api/v2/catalog_items.json")]
	Task<List<CatalogItem>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific catalog item by ID.
	/// </summary>
	/// <param name="id">The ID of the catalog item.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The catalog item.</returns>
	[Get("/api/v2/catalog_items/{id}.json")]
	Task<CatalogItem> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new catalog item.
	/// </summary>
	/// <param name="catalogItem">The catalog item to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created catalog item.</returns>
	[Post("/api/v2/catalog_items.json")]
	Task<CatalogItem> CreateAsync([Body] CatalogItem catalogItem, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing catalog item.
	/// </summary>
	/// <param name="id">The ID of the catalog item to update.</param>
	/// <param name="catalogItem">The catalog item data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated catalog item.</returns>
	[Put("/api/v2/catalog_items/{id}.json")]
	Task<CatalogItem> UpdateAsync(int id, [Body] CatalogItem catalogItem, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a catalog item.
	/// </summary>
	/// <param name="id">The ID of the catalog item to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/catalog_items/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}