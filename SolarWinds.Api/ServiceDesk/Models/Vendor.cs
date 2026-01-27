using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk vendor.
/// </summary>
public class Vendor : NamedEntity
{
	/// <summary>
	/// Gets or sets the vendor contact person.
	/// </summary>
	public required string ContactPerson { get; set; }

	/// <summary>
	/// Gets or sets the vendor email.
	/// </summary>
	public required string Email { get; set; }

	/// <summary>
	/// Gets or sets the vendor phone number.
	/// </summary>
	public required string Phone { get; set; }

	/// <summary>
	/// Gets or sets the vendor website.
	/// </summary>
	public required string Website { get; set; }
}