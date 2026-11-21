using System.Text.Json.Serialization;

namespace SolarWinds.Api.Orion;

/// <summary>
/// A Poller
/// </summary>
public class Poller : Entity
{
	/// <summary>
	/// The PollerID
	/// </summary>
	[JsonPropertyName("PollerID")]
	public int PollerID { get; set; }

	/// <summary>
	/// The PollerType
	/// </summary>
	[JsonPropertyName("PollerType")]
	public required string PollerType { get; set; }

	/// <summary>
	/// The NetObject
	/// </summary>
	[JsonPropertyName("NetObject")]
	public required string NetObject { get; set; }

	/// <summary>
	/// The NetObjectType
	/// </summary>
	[JsonPropertyName("NetObjectType")]
	public required string NetObjectType { get; set; }

	/// <summary>
	/// The NetObjectID
	/// </summary>
	[JsonPropertyName("NetObjectID")]
	public required string NetObjectID { get; set; }

	/// <summary>
	/// Whether it is enabled
	/// </summary>
	[JsonPropertyName("Enabled")]
	public required string Enabled { get; set; }
}
