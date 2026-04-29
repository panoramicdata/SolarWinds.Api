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
	public string State { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the root cause.
	/// </summary>
	public string RootCause { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the symptoms.
	/// </summary>
	public string Symptoms { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the workaround.
	/// </summary>
	public string Workaround { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the problem priority.
	/// </summary>
	public string Priority { get; set; } = string.Empty;
}