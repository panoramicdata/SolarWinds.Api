using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk release.
/// </summary>
public class Release : WorkItemEntity
{
	/// <summary>
	/// Gets or sets the release number payload.
	/// </summary>
	public object? Number { get; set; }

	/// <summary>
	/// Gets or sets the release state.
	/// </summary>
	public string State { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the site.
	/// </summary>
	public object? Site { get; set; }

	/// <summary>
	/// Gets or sets the department.
	/// </summary>
	public object? Department { get; set; }

	/// <summary>
	/// Gets or sets the group assignee's name.
	/// </summary>
	public string GroupAssignee { get; set; } = string.Empty;
}