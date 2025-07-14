using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk role.
/// </summary>
public class Role
{
	/// <summary>
	/// Gets or sets the role ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the role name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the role description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets whether the role has portal access.
	/// </summary>
	public bool Portal { get; set; }

	/// <summary>
	/// Gets or sets whether the role shows "My Tasks".
	/// </summary>
	public bool ShowMyTasks { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}