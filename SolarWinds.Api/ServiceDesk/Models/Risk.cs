using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk risk.
/// </summary>
public class Risk
{
	/// <summary>
	/// Gets or sets the risk ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the risk name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the risk description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the risk level.
	/// </summary>
	public string Level { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}