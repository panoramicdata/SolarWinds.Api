using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk release.
/// </summary>
public class Release : WorkItemEntity
{
	/// <summary>
	/// Gets or sets the release number.
	/// </summary>
	public required string Number { get; set; }

	/// <summary>
	/// Gets or sets the release state.
	/// </summary>
	public required string State { get; set; }

	/// <summary>
	/// Gets or sets the site.
	/// </summary>
	public required string Site { get; set; }

	/// <summary>
	/// Gets or sets the department.
	/// </summary>
	public required string Department { get; set; }

	/// <summary>
	/// Gets or sets the group assignee's name.
	/// </summary>
	public required string GroupAssignee { get; set; }
}