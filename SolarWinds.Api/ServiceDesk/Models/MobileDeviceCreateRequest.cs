namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a mobile device.
/// </summary>
public sealed class MobileDeviceCreateRequest
{
	/// <summary>
	/// Gets or sets the mobile device payload to create.
	/// </summary>
	public MobileDeviceWriteFields Mobile { get; set; } = new();
}
