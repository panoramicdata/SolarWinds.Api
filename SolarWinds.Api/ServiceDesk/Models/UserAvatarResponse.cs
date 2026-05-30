namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a single user avatar result.
/// </summary>
public sealed class UserAvatarResponse
{
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	[JsonPropertyName("avatar")]
	public string? Avatar { get; set; }

	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}
