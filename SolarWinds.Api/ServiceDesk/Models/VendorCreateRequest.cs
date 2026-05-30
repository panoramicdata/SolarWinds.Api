namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a vendor.
/// </summary>
public sealed class VendorCreateRequest
{
	/// <summary>
	/// Gets or sets the vendor payload to create.
	/// </summary>
	public VendorWriteFields Vendor { get; set; } = new();
}
