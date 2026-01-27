using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk user.
/// </summary>
public class User : BaseEntity
{
	/// <summary>
	/// Gets or sets the user's name.
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Gets or sets the user's email address.
	/// </summary>
	public required string Email { get; set; }

	/// <summary>
	/// Gets or sets the user's role.
	/// </summary>
	public required string Role { get; set; }

	/// <summary>
	/// Gets or sets whether the user is active.
	/// </summary>
	public bool Active { get; set; }
}