namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a configuration item.
/// </summary>
public sealed class ConfigurationItemCreateRequest
{
	public ConfigurationItemWriteFields ConfigurationItem { get; set; } = new();
}
