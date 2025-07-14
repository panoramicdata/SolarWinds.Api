using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk task.
/// </summary>
public class ServiceTask
{
	/// <summary>
	/// Gets or sets the task ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the task name/subject.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the task description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the task status.
	/// </summary>
	public string Status { get; set; }

	/// <summary>
	/// Gets or sets the due date.
	/// </summary>
	public DateTime DueDate { get; set; }

	/// <summary>
	/// Gets or sets the assignee's email.
	/// </summary>
	public string Assignee { get; set; }

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