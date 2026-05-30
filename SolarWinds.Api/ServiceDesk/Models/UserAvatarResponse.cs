namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a single user avatar result.
/// </summary>
public sealed class UserAvatarResponse
{
	/// <summary>
	/// Gets or sets user identifier.
	/// </summary>
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	/// <summary>
	/// Gets or sets avatar payload or avatar URL.
	/// </summary>
	[JsonPropertyName("avatar")]
	public string? Avatar { get; set; }

	/// <summary>
	/// Gets or sets additional unmodeled response fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}
