using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk category.
/// </summary>
public class Category : NamedEntity
{
	/// <summary>
	/// Gets or sets the default tags for the category.
	/// </summary>
	public string DefaultTags { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the parent category ID.
	/// </summary>
	public int ParentId { get; set; }

	/// <summary>
	/// Gets or sets the default assignee ID for the category.
	/// </summary>
	public int DefaultAssigneeId { get; set; }

	/// <summary>
	/// Gets or sets the default group assignee ID for the category.
	/// </summary>
	public int DefaultGroupAssigneeId { get; set; }

	/// <summary>
	/// Gets or sets whether the category is deleted.
	/// </summary>
	public bool Deleted { get; set; }
}