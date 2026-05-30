namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a configuration item.
/// </summary>
public sealed class ConfigurationItemUpdateRequest
{
	/// <summary>
	/// Gets or sets the configuration-item fields to update.
	/// </summary>
	public ConfigurationItemWriteFields ConfigurationItem { get; set; } = new();
}
