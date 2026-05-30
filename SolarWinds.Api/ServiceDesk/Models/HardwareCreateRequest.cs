namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a hardware asset.
/// </summary>
public sealed class HardwareCreateRequest
{
	public HardwareWriteFields Hardware { get; set; } = new();
}
