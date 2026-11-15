namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Incident fields verified to work with update operations.
/// </summary>
public class IncidentUpdateFields : ServiceDeskRecordFields
{
	/// <summary>
	/// Gets or sets state identifier.
	/// </summary>
	public int? StateId { get; set; }

	/// <summary>
	/// Gets or sets incident priority.
	/// </summary>
	public string? Priority { get; set; }

	/// <summary>
	/// Gets or sets related configuration-item identifiers.
	/// </summary>
	public int[]? ConfigurationItemIds { get; set; }

	/// <summary>
	/// Gets or sets CC email recipients.
	/// </summary>
	public string[]? Cc { get; set; }
}
