namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a vendor.
/// </summary>
public sealed class VendorUpdateRequest
{
	/// <summary>
	/// Gets or sets the vendor fields to update.
	/// </summary>
	public VendorWriteFields Vendor { get; set; } = new();
}
