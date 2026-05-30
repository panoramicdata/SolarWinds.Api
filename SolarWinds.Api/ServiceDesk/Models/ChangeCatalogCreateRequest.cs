namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a change catalog.
/// </summary>
public sealed class ChangeCatalogCreateRequest
{
	/// <summary>
	/// Change catalog fields to create.
	/// </summary>
	public ChangeCatalogWriteFields ChangeCatalog { get; set; } = new();
}
