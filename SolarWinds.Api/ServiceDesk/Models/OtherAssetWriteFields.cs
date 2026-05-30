namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable other asset fields used by create and update operations.
/// </summary>
public sealed class OtherAssetWriteFields
{
	public string? Name { get; set; }
	public string? AssetId { get; set; }
	public object? Type { get; set; }
}
