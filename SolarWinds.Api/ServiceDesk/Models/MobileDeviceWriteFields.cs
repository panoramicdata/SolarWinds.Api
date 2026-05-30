namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable mobile device fields used by create and update operations.
/// </summary>
public sealed class MobileDeviceWriteFields
{
	public string? Name { get; set; }
	public string? Imei { get; set; }
	public string? SerialNumber { get; set; }
	public string? Os { get; set; }
	public string? PhoneNumber { get; set; }
	public int? SiteId { get; set; }
	public int? DepartmentId { get; set; }
	public int? UserId { get; set; }
}
