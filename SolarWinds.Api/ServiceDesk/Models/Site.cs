using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk site.
/// </summary>
public class Site : DescribedEntity
{
	/// <summary>
	/// Gets or sets the site location.
	/// </summary>
	public required string Location { get; set; }

	/// <summary>
	/// Gets or sets the site time zone.
	/// </summary>
	public required string TimeZone { get; set; }

	/// <summary>
	/// Gets or sets the default assignee ID for the site.
	/// </summary>
	public int DefaultAssigneeId { get; set; }

	/// <summary>
	/// Gets or sets the default group assignee ID for the site.
	/// </summary>
	public int DefaultGroupAssigneeId { get; set; }
}