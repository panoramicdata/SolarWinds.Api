using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk problem.
/// </summary>
public class Problem
{
	/// <summary>
	/// Gets or sets the problem ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the problem name/subject.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the problem description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the problem state.
	/// </summary>
	public string State { get; set; }

	/// <summary>
	/// Gets or sets the root cause.
	/// </summary>
	public string RootCause { get; set; }

	/// <summary>
	/// Gets or sets the symptoms.
	/// </summary>
	public string Symptoms { get; set; }

	/// <summary>
	/// Gets or sets the workaround.
	/// </summary>
	public string Workaround { get; set; }

	/// <summary>
	/// Gets or sets the assignee's email.
	/// </summary>
	public string Assignee { get; set; }

	/// <summary>
	/// Gets or sets the requester's email.
	/// </summary>
	public string Requester { get; set; }

	/// <summary>
	/// Gets or sets the problem priority.
	/// </summary>
	public string Priority { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}