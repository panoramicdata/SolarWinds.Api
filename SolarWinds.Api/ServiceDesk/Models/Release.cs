using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk release.
/// </summary>
public class Release
{
	/// <summary>
	/// Gets or sets the release ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the release number.
	/// </summary>
	public string Number { get; set; }

	/// <summary>
	/// Gets or sets the release name/subject.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the release description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the release state.
	/// </summary>
	public string State { get; set; }

	/// <summary>
	/// Gets or sets the site.
	/// </summary>
	public string Site { get; set; }

	/// <summary>
	/// Gets or sets the department.
	/// </summary>
	public string Department { get; set; }

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
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}