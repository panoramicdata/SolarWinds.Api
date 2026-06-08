namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// A user reference used in various payloads to represent a user by their email address
/// </summary>
public class UserReference
{
	/// <summary>
	/// The email address of the user
	/// </summary>
	public required string Email { get; set; }
}