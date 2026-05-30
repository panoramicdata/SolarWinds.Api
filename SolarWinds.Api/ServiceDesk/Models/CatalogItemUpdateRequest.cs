namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a catalog item.
/// </summary>
public sealed class CatalogItemUpdateRequest
{
	/// <summary>
	/// Gets or sets the catalog-item fields to update.
	/// </summary>
	public CatalogItemWriteFields CatalogItem { get; set; } = new();
}
