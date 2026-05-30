namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable release fields used by create and update operations.
/// </summary>
public sealed class ReleaseWriteFields
{
	public string? Name { get; set; }
	public int? SiteId { get; set; }
	public int? DepartmentId { get; set; }
	public string? Description { get; set; }
	public int? State { get; set; }
	public string? Plan { get; set; }
	public string? Build { get; set; }
	public string? Deploy { get; set; }
	public DateTime? PlannedStartAt { get; set; }
	public DateTime? PlannedEndAt { get; set; }
	public object? Assignee { get; set; }
	public object? GroupAssignee { get; set; }
	public object? Requester { get; set; }
	public string? Priority { get; set; }
	public string? AddToTagList { get; set; }
	public string? RemoveFromTagList { get; set; }
	public string? TagList { get; set; }
	public object[]? CustomFieldsValuesAttributes { get; set; }
	public object? CustomFieldsValues { get; set; }
	public object[]? ApprovalLevelsAttributes { get; set; }
	public int[]? ItsmChangeIds { get; set; }
	public int[]? ConfigurationItemIds { get; set; }
	public object[]? Incidents { get; set; }
	public object[]? Problems { get; set; }
	public object[]? Changes { get; set; }
	public object[]? Solutions { get; set; }
	public object[]? Releases { get; set; }
	public object[]? PurchaseOrders { get; set; }
	public object[]? ConfigurationItems { get; set; }
}
