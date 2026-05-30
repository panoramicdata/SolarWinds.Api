namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable category fields used by create and update operations.
/// </summary>
public sealed class CategoryWriteFields
{
	/// <summary>
	/// Gets or sets the category name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets default tags applied to items in this category.
	/// </summary>
	public string? DefaultTags { get; set; }

	/// <summary>
	/// Gets or sets parent category identifier.
	/// </summary>
	public int? ParentId { get; set; }

	/// <summary>
	/// Gets or sets default assignee identifier.
	/// </summary>
	public int? DefaultAssigneeId { get; set; }

	/// <summary>
	/// Gets or sets default group assignee identifier.
	/// </summary>
	public int? DefaultGroupAssigneeId { get; set; }

	/// <summary>
	/// Gets or sets whether the category is marked deleted.
	/// </summary>
	public bool? Deleted { get; set; }
}
