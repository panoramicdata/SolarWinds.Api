namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable vendor fields used by create and update operations.
/// </summary>
public sealed class VendorWriteFields
{
	public string? Name { get; set; }
	public string? ContactPerson { get; set; }
	public string? Email { get; set; }
	public string? Phone { get; set; }
	public string? Website { get; set; }
}
