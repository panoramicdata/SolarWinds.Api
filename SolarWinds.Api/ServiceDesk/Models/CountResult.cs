namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a simple count result returned by count endpoints.
/// </summary>
public class CountResult
{
	/// <summary>
	/// Gets or sets the count value.
	/// </summary>
	[JsonPropertyName("count")]
	public int Count { get; set; }

	/// <summary>
	/// Gets or sets an alternate total_count value some endpoints use.
	/// </summary>
	[JsonPropertyName("total_count")]
	public int? TotalCount { get; set; }

	/// <summary>
	/// Gets the effective count, preferring <see cref="TotalCount"/> if present.
	/// </summary>
	[JsonIgnore]
	public int EffectiveCount => TotalCount ?? Count;
}
