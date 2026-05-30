namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a warranty.
/// </summary>
public sealed class WarrantyCreateRequest
{
	public WarrantyWriteFields Warranty { get; set; } = new();
}
