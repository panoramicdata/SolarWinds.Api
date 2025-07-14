namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk user.
/// </summary>
public class User
{
	/// <summary>
	/// Gets or sets the user ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the user's name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the user's email address.
	/// </summary>
	public string Email { get; set; }

	/// <summary>
	/// Gets or sets the user's role.
	/// </summary>
	public string Role { get; set; }

	/// <summary>
	/// Gets or sets whether the user is active.
	/// </summary>
	public bool Active { get; set; }
}