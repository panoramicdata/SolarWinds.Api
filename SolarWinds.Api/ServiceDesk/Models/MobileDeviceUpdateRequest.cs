namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a mobile device.
/// </summary>
public sealed class MobileDeviceUpdateRequest
{
	/// <summary>
	/// Gets or sets the mobile device fields to update.
	/// </summary>
	public MobileDeviceWriteFields Mobile { get; set; } = new();
}
