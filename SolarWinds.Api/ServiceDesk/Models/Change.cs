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
	public required string State { get; set; }

	/// <summary>
	/// Gets or sets the change priority.
	/// </summary>
	public required string Priority { get; set; }

	/// <summary>
	/// Gets or sets the group assignee's name.
	/// </summary>
	public required string GroupAssignee { get; set; }
}