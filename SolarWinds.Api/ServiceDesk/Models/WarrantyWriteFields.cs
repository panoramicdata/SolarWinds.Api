namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable warranty fields used by create and update operations.
/// </summary>
public sealed class WarrantyWriteFields
{
	public string? Service { get; set; }
	public string? Provider { get; set; }
	public string? StartDate { get; set; }
	public string? EndDate { get; set; }
	public string? Status { get; set; }
	public bool? Manual { get; set; }
}
