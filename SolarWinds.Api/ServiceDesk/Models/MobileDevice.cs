using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk mobile device.
/// </summary>
public class MobileDevice : NamedEntity
{
	/// <summary>
	/// Gets or sets the IMEI.
	/// </summary>
	public string Imei { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the serial number.
	/// </summary>
	public string SerialNumber { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the operating system.
	/// </summary>
	public string Os { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the phone number.
	/// </summary>
	public string PhoneNumber { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the site ID.
	/// </summary>
	public int SiteId { get; set; }

	/// <summary>
	/// Gets or sets the department ID.
	/// </summary>
	public int DepartmentId { get; set; }

	/// <summary>
	/// Gets or sets the user ID.
	/// </summary>
	public int UserId { get; set; }
}