using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk solution.
/// </summary>
public class Solution
{
	/// <summary>
	/// Gets or sets the solution ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the solution title.
	/// </summary>
	public string Title { get; set; }

	/// <summary>
	/// Gets or sets the solution content.
	/// </summary>
	public string Content { get; set; }

	/// <summary>
	/// Gets or sets the solution status.
	/// </summary>
	public string Status { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}