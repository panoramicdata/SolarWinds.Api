namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a mobile device.
/// </summary>
public sealed class MobileDeviceUpdateRequest
{
	public MobileDeviceWriteFields Mobile { get; set; } = new();
}
