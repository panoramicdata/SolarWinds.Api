namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable other asset fields used by create and update operations.
/// </summary>
public sealed class OtherAssetWriteFields
{
	/// <summary>
	/// Gets or sets the asset name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets the asset identifier value.
	/// </summary>
	public string? AssetId { get; set; }

	/// <summary>
	/// Gets or sets the asset type payload expected by the API.
	/// </summary>
	public object? Type { get; set; }
}
