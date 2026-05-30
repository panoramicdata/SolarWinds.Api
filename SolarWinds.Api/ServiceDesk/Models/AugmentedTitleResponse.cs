namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Augmented title entry for a requested id.
/// </summary>
public sealed class AugmentedTitleResponse
{
	/// <summary>
	/// Record identifier.
	/// </summary>
	[JsonPropertyName("id")]
	public int Id { get; set; }

	/// <summary>
	/// Augmented title text.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; set; }
}
