namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable printer fields used by update operations.
/// </summary>
public sealed class PrinterWriteFields
{
	/// <summary>
	/// Gets or sets the printer name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets the printer asset tag.
	/// </summary>
	public string? AssetTag { get; set; }

	/// <summary>
	/// Gets or sets the printer IP address.
	/// </summary>
	public string? IpAddress { get; set; }
}
