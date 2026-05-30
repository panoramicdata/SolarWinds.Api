using Refit;
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
	[Get("/catalog_items.json")]
	public Task<List<CatalogItem>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific catalog item by ID.
	/// </summary>
	/// <param name="id">The ID of the catalog item.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The catalog item.</returns>
	[Get("/catalog_items/{id}.json")]
	public Task<CatalogItem> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new catalog item.
	/// </summary>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created catalog item.</returns>
	[Post("/catalog_items.json")]
	public Task<CatalogItem> CreateAsync([Body] CatalogItemCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing catalog item.
	/// </summary>
	/// <param name="id">The ID of the catalog item to update.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated catalog item.</returns>
	[Put("/catalog_items/{id}.json")]
	public Task<CatalogItem> UpdateAsync(int id, [Body] CatalogItemUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a catalog item.
	/// </summary>
	/// <param name="id">The ID of the catalog item to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/catalog_items/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}