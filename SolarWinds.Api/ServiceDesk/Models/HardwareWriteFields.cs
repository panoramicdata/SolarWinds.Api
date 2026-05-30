namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable hardware fields used by create and update operations.
/// </summary>
public sealed class HardwareWriteFields
{
	public string? Name { get; set; }
	public string? AssetTag { get; set; }
	public string? IpAddress { get; set; }
}
