namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a warranty.
/// </summary>
public sealed class WarrantyUpdateRequest
{
	/// <summary>
	/// Gets or sets the warranty fields to update.
	/// </summary>
	public WarrantyWriteFields Warranty { get; set; } = new();
}
