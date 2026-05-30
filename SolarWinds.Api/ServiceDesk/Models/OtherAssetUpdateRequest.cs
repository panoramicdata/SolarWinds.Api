namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating an other asset.
/// </summary>
public sealed class OtherAssetUpdateRequest
{
	public OtherAssetWriteFields OtherAsset { get; set; } = new();
}
