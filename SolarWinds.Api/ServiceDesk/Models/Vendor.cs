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
	public string ContactPerson { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the vendor email.
	/// </summary>
	public string Email { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the vendor phone number.
	/// </summary>
	public string Phone { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the vendor website.
	/// </summary>
	public string Website { get; set; } = string.Empty;
}