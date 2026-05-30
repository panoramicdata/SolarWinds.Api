namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a hardware warranty.
/// </summary>
public class Warranty : Entity
{
	public string? Service { get; set; }
	public string? Provider { get; set; }
	public string? StartDate { get; set; }
	public string? EndDate { get; set; }
	public string? Status { get; set; }
	public bool? Manual { get; set; }
}
