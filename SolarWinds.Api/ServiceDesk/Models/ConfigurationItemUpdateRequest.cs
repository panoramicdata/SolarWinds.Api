namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a configuration item.
/// </summary>
public sealed class ConfigurationItemUpdateRequest
{
	public ConfigurationItemWriteFields ConfigurationItem { get; set; } = new();
}
