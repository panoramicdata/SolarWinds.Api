namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a vendor.
/// </summary>
public sealed class VendorCreateRequest
{
	public VendorWriteFields Vendor { get; set; } = new();
}
