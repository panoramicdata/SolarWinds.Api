using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk audit.
/// </summary>
public class Audit
{
	/// <summary>
	/// Gets or sets the audit ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the audit name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the audit description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}