namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable change catalog fields used by create and update operations.
/// </summary>
public sealed class ChangeCatalogWriteFields
{
	public string? Name { get; set; }
	public int? ChangeType { get; set; }
	public int? SiteId { get; set; }
	public int? DepartmentId { get; set; }
	public string? Description { get; set; }
	public int? StateId { get; set; }
	public int? DefaultAssigneeId { get; set; }
	public int? DefaultGroupAssigneeId { get; set; }
	public string? Priority { get; set; }
	public string? ChangePlan { get; set; }
	public string? RollbackPlan { get; set; }
	public string? TestPlan { get; set; }
	public string? AddToTagList { get; set; }
	public string? RemoveFromTagList { get; set; }
	public string? TagList { get; set; }
	public object[]? CustomFieldsValuesAttributes { get; set; }
	public object? CustomFieldsValues { get; set; }
	public bool? ShowInPortal { get; set; }
}
