namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a catalog item.
/// </summary>
public sealed class CatalogItemCreateRequest
{
	public CatalogItemWriteFields CatalogItem { get; set; } = new();
}
