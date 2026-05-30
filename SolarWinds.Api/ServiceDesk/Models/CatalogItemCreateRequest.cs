namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a catalog item.
/// </summary>
public sealed class CatalogItemCreateRequest
{
	/// <summary>
	/// Gets or sets the catalog-item payload to create.
	/// </summary>
	public CatalogItemWriteFields CatalogItem { get; set; } = new();
}
