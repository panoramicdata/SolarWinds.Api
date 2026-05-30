namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query options for configuration item endpoints.
/// </summary>
public sealed class GetConfigurationItemsRequest
{
	public ResponseLayout? Layout { get; set; }
}
