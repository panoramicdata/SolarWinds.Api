using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a generic Service Desk asset.
/// </summary>
public class Asset : BaseEntity
{
	/// <summary>
	/// Gets or sets the asset name.
	/// </summary>
	public required string Name { get; set; }

	/// <summary>
	/// Gets or sets the asset tag/identifier.
	/// </summary>
	public required string AssetId { get; set; }

	/// <summary>
	/// Gets or sets the asset type (e.g., "Other Asset", "Hardware", "Mobile Device").
	/// </summary>
	public required string Type { get; set; }
}