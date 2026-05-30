namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating an other asset.
/// </summary>
public sealed class OtherAssetCreateRequest
{
	/// <summary>
	/// Gets or sets the other-asset payload to create.
	/// </summary>
	public OtherAssetWriteFields OtherAsset { get; set; } = new();
}
