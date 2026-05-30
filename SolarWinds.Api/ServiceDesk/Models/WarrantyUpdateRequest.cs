namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a warranty.
/// </summary>
public sealed class WarrantyUpdateRequest
{
	public WarrantyWriteFields Warranty { get; set; } = new();
}
