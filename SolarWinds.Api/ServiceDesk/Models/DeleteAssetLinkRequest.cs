namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for deleting an asset link.
/// </summary>
public sealed class DeleteAssetLinkRequest
{
	public int? AssetLinkId { get; set; }
	public int? SourceId { get; set; }
	public string? SourceType { get; set; }
}
