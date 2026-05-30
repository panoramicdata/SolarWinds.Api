namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable mobile device fields used by create and update operations.
/// </summary>
public sealed class MobileDeviceWriteFields
{
	/// <summary>
	/// Gets or sets the mobile asset name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets the IMEI identifier.
	/// </summary>
	public string? Imei { get; set; }

	/// <summary>
	/// Gets or sets the device serial number.
	/// </summary>
	public string? SerialNumber { get; set; }

	/// <summary>
	/// Gets or sets the operating system name/version.
	/// </summary>
	public string? Os { get; set; }

	/// <summary>
	/// Gets or sets the assigned phone number.
	/// </summary>
	public string? PhoneNumber { get; set; }

	/// <summary>
	/// Gets or sets the site identifier.
	/// </summary>
	public int? SiteId { get; set; }

	/// <summary>
	/// Gets or sets the department identifier.
	/// </summary>
	public int? DepartmentId { get; set; }

	/// <summary>
	/// Gets or sets the assigned user identifier.
	/// </summary>
	public int? UserId { get; set; }
}
