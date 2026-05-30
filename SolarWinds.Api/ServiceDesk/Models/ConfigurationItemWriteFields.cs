namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable configuration item fields used by create and update operations.
/// </summary>
public sealed class ConfigurationItemWriteFields
{
	/// <summary>
	/// Gets or sets the configuration item name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets the configuration item description.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets the configuration item type payload.
	/// </summary>
	public object? Type { get; set; }

	/// <summary>
	/// Gets or sets identifiers of dependent assets linked to this item.
	/// </summary>
	public List<int>? DependentAssetIds { get; set; }
}
