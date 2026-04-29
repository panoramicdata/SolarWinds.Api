using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk change.
/// </summary>
public class Change : WorkItemEntity
{
	/// <summary>
	/// Gets or sets the change state.
	/// </summary>
	public string State { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the change priority.
	/// </summary>
	public string Priority { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the group assignee's name.
	/// </summary>
	public string GroupAssignee { get; set; } = string.Empty;
}