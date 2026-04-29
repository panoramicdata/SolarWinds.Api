using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk time track entry.
/// </summary>
public class TimeTrack : TimestampedEntity
{
	/// <summary>
	/// Gets or sets the time spent in minutes.
	/// </summary>
	public int TimeSpent { get; set; }

	/// <summary>
	/// Gets or sets the description of the time track.
	/// </summary>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the ID of the associated incident, problem, or change.
	/// </summary>
	public int CommentableId { get; set; }

	/// <summary>
	/// Gets or sets the type of the associated entity (e.g., "Incident", "Problem", "Change").
	/// </summary>
	public string CommentableType { get; set; } = string.Empty;
}