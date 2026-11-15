namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable incident fields used by create and update operations.
/// </summary>
public sealed class IncidentWriteFields : IncidentUpdateFields
{
	/// <summary>
	/// Gets or sets state value.
	/// </summary>
	public string? State { get; set; }

	/// <summary>
	/// Gets or sets incident due date/time.
	/// </summary>
	public DateTime? DueAt { get; set; }

	/// <summary>
	/// Gets or sets custom field value payload.
	/// </summary>
	public object? CustomFieldsValues { get; set; }

	/// <summary>
	/// The assignee
	/// </summary>
	public UserReference? Assignee { get; set; }

	/// <summary>
	/// The requester
	/// </summary>
	public UserReference? Requester { get; set; }
}
