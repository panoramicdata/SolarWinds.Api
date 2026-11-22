using System.Text.Json.Serialization;

namespace SolarWinds.Api.Orion;

/// <summary>
/// A Managed entity
/// </summary>
public abstract class ManagedEntity : Entity
{
	/// <summary>
	/// Status
	/// </summary>
	[JsonPropertyName("Status")]
	public int Status { get; set; }

	/// <summary>
	/// StatusLED
	/// </summary>
	[JsonPropertyName("StatusLED")]
	public required string StatusLED { get; set; }

	/// <summary>
	/// StatusDescription
	/// </summary>
	[JsonPropertyName("StatusDescription")]
	public required string StatusDescription { get; set; }

	/// <summary>
	/// UnManaged
	/// </summary>
	[JsonPropertyName("UnManaged")]
	public bool UnManaged { get; set; }

	/// <summary>
	/// UnManageFrom
	/// </summary>
	[JsonPropertyName("UnManageFrom")]
	public DateTime UnManageFrom { get; set; }

	/// <summary>
	/// UnManageUntil
	/// </summary>
	[JsonPropertyName("UnManageUntil")]
	public DateTime UnManageUntil { get; set; }

	/// <summary>
	/// Image
	/// </summary>
	[JsonPropertyName("Image")]
	public required string Image { get; set; }

	/// <summary>
	/// AncestorDisplayNames
	/// </summary>
	[JsonPropertyName("AncestorDisplayNames")]
	public required List<string> AncestorDisplayNames { get; set; }

	/// <summary>
	/// AncestorDetailsUrls
	/// </summary>
	[JsonPropertyName("AncestorDetailsUrls")]
	public required List<string> AncestorDetailsUrls { get; set; }

	/// <summary>
	/// StatusIconHint
	/// </summary>
	[JsonPropertyName("StatusIconHint")]
	public required string StatusIconHint { get; set; }
}