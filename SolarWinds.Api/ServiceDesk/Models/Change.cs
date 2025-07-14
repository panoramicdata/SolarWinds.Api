using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk change.
/// </summary>
public class Change
{
	/// <summary>
	/// Gets or sets the change ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the change name/subject.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the change description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the change state.
	/// </summary>
	public string State { get; set; }

	/// <summary>
	/// Gets or sets the change priority.
	/// </summary>
	public string Priority { get; set; }

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