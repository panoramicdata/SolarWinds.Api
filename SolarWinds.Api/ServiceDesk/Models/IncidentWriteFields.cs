namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable incident fields used by create and update operations.
/// </summary>
public sealed class IncidentWriteFields
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
	/// Gets or sets state value.
	/// </summary>
	public string? State { get; set; }

	/// <summary>
	/// Gets or sets incident priority.
	/// </summary>
	public string? Priority { get; set; }

	/// <summary>
	/// Gets or sets incident due date/time.
	/// </summary>
	public DateTime? DueAt { get; set; }

	/// <summary>
	/// Gets or sets custom field value payload.
	/// </summary>
	public object? CustomFieldsValues { get; set; }

	/// <summary>
	/// Gets or sets related configuration-item identifiers.
	/// </summary>
	public int[]? ConfigurationItemIds { get; set; }

	/// <summary>
	/// Gets or sets CC email recipients.
	/// </summary>
	public string[]? Cc { get; set; }

	/// <summary>
	/// The assignee
	/// </summary>
	public UserReference? Assignee { get; set; }

	/// <summary>
	/// The requester
	/// </summary>
	public UserReference? Requester { get; set; }
}
