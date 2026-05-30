namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a catalog item.
/// </summary>
public sealed class CatalogItemUpdateRequest
{
	public CatalogItemWriteFields CatalogItem { get; set; } = new();
}
