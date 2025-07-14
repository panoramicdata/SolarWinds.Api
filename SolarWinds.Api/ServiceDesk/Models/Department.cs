using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk department.
/// </summary>
public class Department
{
	/// <summary>
	/// Gets or sets the department ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the department name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the department description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the default assignee ID for the department.
	/// </summary>
	public int DefaultAssigneeId { get; set; }

	/// <summary>
	/// Gets or sets the default group assignee ID for the department.
	/// </summary>
	public int DefaultGroupAssigneeId { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}