namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable printer fields used by update operations.
/// </summary>
public sealed class PrinterWriteFields
{
	public string? Name { get; set; }
	public string? AssetTag { get; set; }
	public string? IpAddress { get; set; }
}
