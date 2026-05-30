namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a hardware asset.
/// </summary>
public sealed class HardwareUpdateRequest
{
	public HardwareWriteFields Hardware { get; set; } = new();
}
