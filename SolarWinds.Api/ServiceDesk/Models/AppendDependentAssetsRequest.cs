namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for appending dependent assets to a configuration item.
/// </summary>
public sealed class AppendDependentAssetsRequest
{
	/// <summary>
	/// Gets or sets dependent asset identifiers to append.
	/// </summary>
	public IEnumerable<int>? AssetIds { get; set; }
}
