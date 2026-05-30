namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable change fields used by create and update operations.
/// </summary>
public sealed class ChangeWriteFields
{
	public string? Name { get; set; }
	public int? ChangeType { get; set; }
	public int? SiteId { get; set; }
	public int? DepartmentId { get; set; }
	public string? Description { get; set; }
	public int? StateId { get; set; }
	public object? Assignee { get; set; }
	public object? GroupAssignee { get; set; }
	public object? Requester { get; set; }
	public string? Priority { get; set; }
	public string? ChangePlan { get; set; }
	public string? RollbackPlan { get; set; }
	public string? TestPlan { get; set; }
	public DateTime? PlannedStartAt { get; set; }
	public DateTime? PlannedEndAt { get; set; }
	public string? AddToTagList { get; set; }
	public string? RemoveFromTagList { get; set; }
	public string? TagList { get; set; }
	public object[]? CustomFieldsValuesAttributes { get; set; }
	public object? CustomFieldsValues { get; set; }
	public int[]? ConfigurationItemIds { get; set; }
	public object[]? Incidents { get; set; }
	public object[]? Problems { get; set; }
	public object[]? Changes { get; set; }
	public object[]? Solutions { get; set; }
	public object[]? Releases { get; set; }
	public object[]? PurchaseOrders { get; set; }
	public object[]? ConfigurationItems { get; set; }
}
