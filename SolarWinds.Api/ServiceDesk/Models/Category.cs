using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk category.
/// </summary>
public class Category
{
	/// <summary>
	/// Gets or sets the category ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the category name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the default tags for the category.
	/// </summary>
	public string DefaultTags { get; set; }

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

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}