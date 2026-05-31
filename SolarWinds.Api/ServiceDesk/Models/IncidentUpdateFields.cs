namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Incident fields verified to work with update operations.
/// </summary>
public sealed class IncidentUpdateFields
{
	/// <summary>
	/// Gets or sets incident title.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets site identifier.
	/// </summary>
	public int? SiteId { get; set; }

	/// <summary>
	/// Gets or sets department identifier.
	/// </summary>
	public int? DepartmentId { get; set; }

	/// <summary>
	/// Gets or sets rich-text incident description.
	/// </summary>
	public string? Description { get; set; }

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