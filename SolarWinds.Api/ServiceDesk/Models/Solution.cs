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
	public required string Title { get; set; }

	/// <summary>
	/// Gets or sets the solution content.
	/// </summary>
	public required string Content { get; set; }

	/// <summary>
	/// Gets or sets the solution status.
	/// </summary>
	public required string Status { get; set; }
}