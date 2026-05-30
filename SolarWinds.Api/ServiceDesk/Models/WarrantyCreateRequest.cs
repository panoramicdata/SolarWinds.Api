namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a warranty.
/// </summary>
public sealed class WarrantyCreateRequest
{
	/// <summary>
	/// Gets or sets the warranty payload to create.
	/// </summary>
	public WarrantyWriteFields Warranty { get; set; } = new();
}
