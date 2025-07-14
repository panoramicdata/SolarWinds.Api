namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a generic Service Desk asset.
/// </summary>
public class Asset
{
	/// <summary>
	/// Gets or sets the asset ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the asset name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the asset tag/identifier.
	/// </summary>
	public string AssetId { get; set; }

	/// <summary>
	/// Gets or sets the asset type (e.g., "Other Asset", "Hardware", "Mobile Device").
	/// </summary>
	public string Type { get; set; }
}