namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable configuration item fields used by create and update operations.
/// </summary>
public sealed class ConfigurationItemWriteFields
{
	public string? Name { get; set; }
	public string? Description { get; set; }
	public object? Type { get; set; }
	public List<int>? DependentAssetIds { get; set; }
}
