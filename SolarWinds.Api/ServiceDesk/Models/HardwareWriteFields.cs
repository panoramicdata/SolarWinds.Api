namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable hardware fields used by create and update operations.
/// </summary>
public sealed class HardwareWriteFields
{
	/// <summary>
	/// Gets or sets the hardware asset name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets the asset tag value.
	/// </summary>
	public string? AssetTag { get; set; }

	/// <summary>
	/// Gets or sets the hardware IP address.
	/// </summary>
	public string? IpAddress { get; set; }
}
