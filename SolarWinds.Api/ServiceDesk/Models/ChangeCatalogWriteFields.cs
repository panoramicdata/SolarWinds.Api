namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable change catalog fields used by create and update operations.
/// </summary>
public sealed class ChangeCatalogWriteFields
{
	/// <summary>
	/// Gets or sets catalog name.
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
	/// Gets or sets description text.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets state identifier.
	/// </summary>
	public int? StateId { get; set; }

	/// <summary>
	/// Gets or sets default assignee identifier.
	/// </summary>
	public int? DefaultAssigneeId { get; set; }

	/// <summary>
	/// Gets or sets default group assignee identifier.
	/// </summary>
	public int? DefaultGroupAssigneeId { get; set; }

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
	/// Gets or sets whether the catalog should be visible in portal views.
	/// </summary>
	public bool? ShowInPortal { get; set; }
}
