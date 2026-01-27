using System;
using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk incident.
/// </summary>
public class Incident : WorkItemEntity
{
	/// <summary>
	/// Gets or sets the incident number.
	/// </summary>
	public required string Number { get; set; }

	/// <summary>
	/// Gets or sets the incident state ID.
	/// </summary>
	public int StateId { get; set; }

	/// <summary>
	/// Gets or sets the incident priority.
	/// </summary>
	public required string Priority { get; set; }

	/// <summary>
	/// Gets or sets the incident category.
	/// </summary>
	public required string Category { get; set; }

	/// <summary>
	/// Gets or sets the incident subcategory.
	/// </summary>
	public required string Subcategory { get; set; }

	/// <summary>
	/// Gets or sets the group assignee's name.
	/// </summary>
	public required string GroupAssignee { get; set; }

	/// <summary>
	/// Gets or sets the site ID.
	/// </summary>
	public int SiteId { get; set; }

	/// <summary>
	/// Gets or sets the department ID.
	/// </summary>
	public int DepartmentId { get; set; }

	/// <summary>
	/// Gets or sets the due date.
	/// </summary>
	public DateTime DueAt { get; set; }

	/// <summary>
	/// Gets or sets the tag list.
	/// </summary>
	public required string TagList { get; set; }
}