using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk time track entry.
/// </summary>
public class TimeTrack
{
	/// <summary>
	/// Gets or sets the time track ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the time spent in minutes.
	/// </summary>
	public int TimeSpent { get; set; }

	/// <summary>
	/// Gets or sets the description of the time track.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the ID of the associated incident, problem, or change.
	/// </summary>
	public int CommentableId { get; set; }

	/// <summary>
	/// Gets or sets the type of the associated entity (e.g., "Incident", "Problem", "Change").
	/// </summary>
	public string CommentableType { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}