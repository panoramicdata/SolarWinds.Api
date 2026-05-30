namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a configuration item.
/// </summary>
public sealed class ConfigurationItemCreateRequest
{
	/// <summary>
	/// Gets or sets the configuration-item payload to create.
	/// </summary>
	public ConfigurationItemWriteFields ConfigurationItem { get; set; } = new();
}
