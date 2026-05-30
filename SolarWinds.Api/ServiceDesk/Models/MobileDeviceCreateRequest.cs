namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a mobile device.
/// </summary>
public sealed class MobileDeviceCreateRequest
{
	public MobileDeviceWriteFields Mobile { get; set; } = new();
}
