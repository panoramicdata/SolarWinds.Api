using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk site.
/// </summary>
public class Site
{
	/// <summary>
	/// Gets or sets the site ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the site name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the site location.
	/// </summary>
	public string Location { get; set; }

	/// <summary>
	/// Gets or sets the site description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the site time zone.
	/// </summary>
	public string TimeZone { get; set; }

	/// <summary>
	/// Gets or sets the default assignee ID for the site.
	/// </summary>
	public int DefaultAssigneeId { get; set; }

	/// <summary>
	/// Gets or sets the default group assignee ID for the site.
	/// </summary>
	public int DefaultGroupAssigneeId { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}