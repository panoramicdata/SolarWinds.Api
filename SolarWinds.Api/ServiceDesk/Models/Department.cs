using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk department.
/// </summary>
public class Department : DescribedEntity
{
	/// <summary>
	/// Gets or sets the default assignee ID for the department.
	/// </summary>
	public int DefaultAssigneeId { get; set; }

	/// <summary>
	/// Gets or sets the default group assignee ID for the department.
	/// </summary>
	public int DefaultGroupAssigneeId { get; set; }
}