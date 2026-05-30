namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable vendor fields used by create and update operations.
/// </summary>
public sealed class VendorWriteFields
{
	/// <summary>
	/// Gets or sets vendor name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets vendor contact person.
	/// </summary>
	public string? ContactPerson { get; set; }

	/// <summary>
	/// Gets or sets vendor email address.
	/// </summary>
	public string? Email { get; set; }

	/// <summary>
	/// Gets or sets vendor phone number.
	/// </summary>
	public string? Phone { get; set; }

	/// <summary>
	/// Gets or sets vendor website.
	/// </summary>
	public string? Website { get; set; }
}
