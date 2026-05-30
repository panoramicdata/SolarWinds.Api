namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a hardware asset.
/// </summary>
public sealed class HardwareUpdateRequest
{
	/// <summary>
	/// Gets or sets the hardware fields to update.
	/// </summary>
	public HardwareWriteFields Hardware { get; set; } = new();
}
