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
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the asset tag/identifier.
	/// </summary>
	public string AssetId { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the asset type (e.g., "Other Asset", "Hardware", "Mobile Device").
	/// </summary>
	public object? Type { get; set; }
}