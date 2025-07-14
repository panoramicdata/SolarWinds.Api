using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk mobile device.
/// </summary>
public class MobileDevice
{
	/// <summary>
	/// Gets or sets the mobile device ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the mobile device name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the IMEI.
	/// </summary>
	public string Imei { get; set; }

	/// <summary>
	/// Gets or sets the serial number.
	/// </summary>
	public string SerialNumber { get; set; }

	/// <summary>
	/// Gets or sets the operating system.
	/// </summary>
	public string Os { get; set; }

	/// <summary>
	/// Gets or sets the phone number.
	/// </summary>
	public string PhoneNumber { get; set; }

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

	/// <summary>
	/// Gets or sets custom field values.
	/// </summary>
	public object CustomFieldsValues { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}