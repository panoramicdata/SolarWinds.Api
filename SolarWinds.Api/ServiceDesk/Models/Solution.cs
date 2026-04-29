using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk solution.
/// </summary>
public class Solution : TimestampedEntity
{
	/// <summary>
	/// Gets or sets the solution title.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the solution content.
	/// </summary>
	public string Content { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the solution status.
	/// </summary>
	public string Status { get; set; } = string.Empty;
}