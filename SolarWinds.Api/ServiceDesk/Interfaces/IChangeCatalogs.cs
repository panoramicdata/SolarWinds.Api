using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Change Catalog API.
/// </summary>
public interface IChangeCatalogs
{
	[Get("/change_catalogs.json")]
	public Task<List<ChangeCatalog>> GetAsync(CancellationToken cancellationToken);

	[Get("/change_catalogs/{id}.json")]
	public Task<ChangeCatalog> GetAsync(int id, [AliasAs("layout")] ResponseLayout layout, CancellationToken cancellationToken);

	[Post("/change_catalogs.json")]
	public Task<ChangeCatalog> CreateAsync([Body] ChangeCatalogCreateRequest request, CancellationToken cancellationToken);

	[Put("/change_catalogs/{id}.json")]
	public Task<ChangeCatalog> UpdateAsync(int id, [Body] ChangeCatalogUpdateRequest request, CancellationToken cancellationToken);

	[Delete("/change_catalogs/{id}.json")]
	public Task DeleteAsync(int id, CancellationToken cancellationToken);
}
