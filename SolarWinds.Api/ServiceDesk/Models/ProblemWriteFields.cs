namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable problem fields used by create and update operations.
/// </summary>
public sealed class ProblemWriteFields
{
	/// <summary>
	/// Gets or sets problem title.
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
	/// Gets or sets problem description.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets state identifier.
	/// </summary>
	public int? State { get; set; }

	/// <summary>
	/// Gets or sets root cause details.
	/// </summary>
	public string? RootCause { get; set; }

	/// <summary>
	/// Gets or sets symptom details.
	/// </summary>
	public string? Symptoms { get; set; }

	/// <summary>
	/// Gets or sets workaround details.
	/// </summary>
	public string? Workaround { get; set; }

	/// <summary>
	/// Gets or sets assignee payload.
	/// </summary>
	public object? Assignee { get; set; }

	/// <summary>
	/// Gets or sets group-assignee payload.
	/// </summary>
	public object? GroupAssignee { get; set; }

	/// <summary>
	/// Gets or sets requester payload.
	/// </summary>
	public object? Requester { get; set; }

	/// <summary>
	/// Gets or sets problem priority.
	/// </summary>
	public string? Priority { get; set; }

	/// <summary>
	/// Gets or sets tags to append.
	/// </summary>
	public string? AddToTagList { get; set; }

	/// <summary>
	/// Gets or sets tags to remove.
	/// </summary>
	public string? RemoveFromTagList { get; set; }

	/// <summary>
	/// Gets or sets full replacement tag list.
	/// </summary>
	public string? TagList { get; set; }

	/// <summary>
	/// Gets or sets custom field value attribute payload.
	/// </summary>
	public object[]? CustomFieldsValuesAttributes { get; set; }

	/// <summary>
	/// Gets or sets custom field value payload.
	/// </summary>
	public object? CustomFieldsValues { get; set; }

	/// <summary>
	/// Gets or sets linked incident identifiers.
	/// </summary>
	public int[]? IncidentIds { get; set; }

	/// <summary>
	/// Gets or sets linked ITSM change identifiers.
	/// </summary>
	public int[]? ItsmChangeIds { get; set; }

	/// <summary>
	/// Gets or sets linked configuration item identifiers.
	/// </summary>
	public int[]? ConfigurationItemIds { get; set; }

	/// <summary>
	/// Gets or sets related incident link payload.
	/// </summary>
	public object[]? Incidents { get; set; }

	/// <summary>
	/// Gets or sets related problem link payload.
	/// </summary>
	public object[]? Problems { get; set; }

	/// <summary>
	/// Gets or sets related change link payload.
	/// </summary>
	public object[]? Changes { get; set; }

	/// <summary>
	/// Gets or sets related solution link payload.
	/// </summary>
	public object[]? Solutions { get; set; }

	/// <summary>
	/// Gets or sets related release link payload.
	/// </summary>
	public object[]? Releases { get; set; }

	/// <summary>
	/// Gets or sets related purchase-order link payload.
	/// </summary>
	public object[]? PurchaseOrders { get; set; }

	/// <summary>
	/// Gets or sets related configuration-item link payload.
	/// </summary>
	public object[]? ConfigurationItems { get; set; }
}
