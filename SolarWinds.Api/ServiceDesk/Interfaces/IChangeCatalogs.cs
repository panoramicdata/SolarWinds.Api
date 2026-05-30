using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Change Catalog API.
/// </summary>
public interface IChangeCatalogs
{
	/// <summary>
	/// Lists change catalogs.
	/// </summary>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>All visible change catalogs.</returns>
	[Get("/change_catalogs.json")]
	public Task<List<ChangeCatalog>> GetAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Lists change catalogs using query filters.
	/// </summary>
	/// <param name="request">Filter and projection parameters.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>Filtered change catalogs.</returns>
	[Get("/change_catalogs.json")]
	public Task<List<ChangeCatalog>> GetAsync([Query] GetChangeCatalogsRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific change catalog by identifier.
	/// </summary>
	/// <param name="id">Change catalog identifier.</param>
	/// <param name="layout">Desired response layout.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>The requested change catalog.</returns>
	[Get("/change_catalogs/{id}.json")]
	public Task<ChangeCatalog> GetAsync(int id, [AliasAs("layout")] ResponseLayout layout, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new change catalog.
	/// </summary>
	/// <param name="request">Change catalog creation payload.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>The created change catalog.</returns>
	[Post("/change_catalogs.json")]
	public Task<ChangeCatalog> CreateAsync([Body] ChangeCatalogCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing change catalog.
	/// </summary>
	/// <param name="id">Change catalog identifier.</param>
	/// <param name="request">Change catalog update payload.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>The updated change catalog.</returns>
	[Put("/change_catalogs/{id}.json")]
	public Task<ChangeCatalog> UpdateAsync(int id, [Body] ChangeCatalogUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a change catalog.
	/// </summary>
	/// <param name="id">Change catalog identifier.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	[Delete("/change_catalogs/{id}.json")]
	public Task DeleteAsync(int id, CancellationToken cancellationToken);
}
