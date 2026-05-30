namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for deleting an asset link.
/// </summary>
public sealed class DeleteAssetLinkRequest
{
	/// <summary>
	/// Gets or sets the asset-link identifier to remove.
	/// </summary>
	public int? AssetLinkId { get; set; }

	/// <summary>
	/// Gets or sets the source object identifier that owns the link.
	/// </summary>
	public int? SourceId { get; set; }

	/// <summary>
	/// Gets or sets the source object type.
	/// </summary>
	public string? SourceType { get; set; }
}
