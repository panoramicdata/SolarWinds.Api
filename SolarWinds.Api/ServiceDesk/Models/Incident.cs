using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk incident.
/// </summary>
public class Incident
{
	/// <summary>
	/// Gets or sets the incident ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the incident number.
	/// </summary>
	public string Number { get; set; }

	/// <summary>
	/// Gets or sets the incident name/subject.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the incident description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the incident state ID.
	/// </summary>
	public int StateId { get; set; }

	/// <summary>
	/// Gets or sets the incident priority.
	/// </summary>
	public string Priority { get; set; }

	/// <summary>
	/// Gets or sets the incident category.
	/// </summary>
	public string Category { get; set; }

	/// <summary>
	/// Gets or sets the incident subcategory.
	/// </summary>
	public string Subcategory { get; set; }

	/// <summary>
	/// Gets or sets the assignee's email.
	/// </summary>
	public string Assignee { get; set; }

	/// <summary>
	/// Gets or sets the group assignee's name.
	/// </summary>
	public string GroupAssignee { get; set; }

	/// <summary>
	/// Gets or sets the requester's email.
	/// </summary>
	public string Requester { get; set; }

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
	public string TagList { get; set; }

	/// <summary>
	/// Gets or sets custom field values.
	/// </summary>
	public object CustomFieldsValues { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}