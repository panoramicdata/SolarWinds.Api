using System.Text.Json.Serialization;

namespace SolarWinds.Api.Orion;

/// <summary>
/// A Service
/// </summary>
public class Service : Entity
{
	/// <summary>
	/// ObjectSubType
	/// </summary>
	[JsonPropertyName("ServiceName")]
	public required string ServiceName { get; set; }

	/// <summary>
	/// Memory
	/// </summary>
	[JsonPropertyName("Memory")]
	public long Memory { get; set; }
}
