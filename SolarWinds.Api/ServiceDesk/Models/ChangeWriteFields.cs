namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable change fields used by create and update operations.
/// </summary>
public sealed class ChangeWriteFields
{
	/// <summary>
	/// Gets or sets change title.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets change type identifier.
	/// </summary>
	public int? ChangeType { get; set; }

	/// <summary>
	/// Gets or sets site identifier.
	/// </summary>
	public int? SiteId { get; set; }

	/// <summary>
	/// Gets or sets department identifier.
	/// </summary>
	public int? DepartmentId { get; set; }

	/// <summary>
	/// Gets or sets change description.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets state identifier.
	/// </summary>
	public int? StateId { get; set; }

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
	/// Gets or sets priority value.
	/// </summary>
	public string? Priority { get; set; }

	/// <summary>
	/// Gets or sets change plan text.
	/// </summary>
	public string? ChangePlan { get; set; }

	/// <summary>
	/// Gets or sets rollback plan text.
	/// </summary>
	public string? RollbackPlan { get; set; }

	/// <summary>
	/// Gets or sets test plan text.
	/// </summary>
	public string? TestPlan { get; set; }

	/// <summary>
	/// Gets or sets planned start time.
	/// </summary>
	public DateTime? PlannedStartAt { get; set; }

	/// <summary>
	/// Gets or sets planned end time.
	/// </summary>
	public DateTime? PlannedEndAt { get; set; }

	/// <summary>
	/// Gets or sets tags to append.
	/// </summary>
	public string? AddToTagList { get; set; }

	/// <summary>
	/// Gets or sets tags to remove.
	/// </summary>
	public string? RemoveFromTagList { get; set; }

	/// <summary>
	/// Gets or sets complete tag list replacement value.
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
	/// Gets or sets related configuration item identifiers.
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
