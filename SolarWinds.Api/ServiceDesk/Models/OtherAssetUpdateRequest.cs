namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating an other asset.
/// </summary>
public sealed class OtherAssetUpdateRequest
{
	/// <summary>
	/// Gets or sets the other-asset fields to update.
	/// </summary>
	public OtherAssetWriteFields OtherAsset { get; set; } = new();
}
