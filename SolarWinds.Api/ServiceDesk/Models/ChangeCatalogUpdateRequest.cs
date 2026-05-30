namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a change catalog.
/// </summary>
public sealed class ChangeCatalogUpdateRequest
{
	/// <summary>
	/// Change catalog fields to update.
	/// </summary>
	public ChangeCatalogWriteFields ChangeCatalog { get; set; } = new();
}
