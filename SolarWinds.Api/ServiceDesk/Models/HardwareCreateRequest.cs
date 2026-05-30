namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a hardware asset.
/// </summary>
public sealed class HardwareCreateRequest
{
	/// <summary>
	/// Gets or sets the hardware payload to create.
	/// </summary>
	public HardwareWriteFields Hardware { get; set; } = new();
}
