namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a vendor.
/// </summary>
public sealed class VendorUpdateRequest
{
	public VendorWriteFields Vendor { get; set; } = new();
}
