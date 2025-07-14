using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk vendor.
/// </summary>
public class Vendor
{
	/// <summary>
	/// Gets or sets the vendor ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the vendor name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the vendor contact person.
	/// </summary>
	public string ContactPerson { get; set; }

	/// <summary>
	/// Gets or sets the vendor email.
	/// </summary>
	public string Email { get; set; }

	/// <summary>
	/// Gets or sets the vendor phone number.
	/// </summary>
	public string Phone { get; set; }

	/// <summary>
	/// Gets or sets the vendor website.
	/// </summary>
	public string Website { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}