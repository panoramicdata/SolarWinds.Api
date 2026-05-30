namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating an other asset.
/// </summary>
public sealed class OtherAssetCreateRequest
{
	public OtherAssetWriteFields OtherAsset { get; set; } = new();
}
