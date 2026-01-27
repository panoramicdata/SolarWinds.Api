using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk problem.
/// </summary>
public class Problem : WorkItemEntity
{
	/// <summary>
	/// Gets or sets the problem state.
	/// </summary>
	public required string State { get; set; }

	/// <summary>
	/// Gets or sets the root cause.
	/// </summary>
	public required string RootCause { get; set; }

	/// <summary>
	/// Gets or sets the symptoms.
	/// </summary>
	public required string Symptoms { get; set; }

	/// <summary>
	/// Gets or sets the workaround.
	/// </summary>
	public required string Workaround { get; set; }

	/// <summary>
	/// Gets or sets the problem priority.
	/// </summary>
	public required string Priority { get; set; }
}