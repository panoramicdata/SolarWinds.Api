namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Fields shared by assignable ITSM records that link to other records.
/// </summary>
public abstract class ItsmRecordFields : TaggedRecordFields
{
	/// <summary>
	/// Gets or sets the assignee payload.
	/// </summary>
	public object? Assignee { get; set; }

	/// <summary>
	/// Gets or sets the group assignee payload.
	/// </summary>
	public object? GroupAssignee { get; set; }

	/// <summary>
	/// Gets or sets the requester payload.
	/// </summary>
	public object? Requester { get; set; }

	/// <summary>
	/// Gets or sets the priority value.
	/// </summary>
	public string? Priority { get; set; }

	/// <summary>
	/// Gets or sets related configuration-item identifiers.
	/// </summary>
	public int[]? ConfigurationItemIds { get; set; }

	/// <summary>
	/// Gets or sets linked incident payloads.
	/// </summary>
	public object[]? Incidents { get; set; }

	/// <summary>
	/// Gets or sets linked problem payloads.
	/// </summary>
	public object[]? Problems { get; set; }

	/// <summary>
	/// Gets or sets linked change payloads.
	/// </summary>
	public object[]? Changes { get; set; }

	/// <summary>
	/// Gets or sets linked solution payloads.
	/// </summary>
	public object[]? Solutions { get; set; }

	/// <summary>
	/// Gets or sets linked release payloads.
	/// </summary>
	public object[]? Releases { get; set; }

	/// <summary>
	/// Gets or sets linked purchase order payloads.
	/// </summary>
	public object[]? PurchaseOrders { get; set; }

	/// <summary>
	/// Gets or sets linked configuration item payloads.
	/// </summary>
	public object[]? ConfigurationItems { get; set; }
}
