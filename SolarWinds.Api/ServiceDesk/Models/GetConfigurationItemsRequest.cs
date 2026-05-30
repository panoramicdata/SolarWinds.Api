namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query options for configuration item endpoints.
/// </summary>
public sealed class GetConfigurationItemsRequest
{
	/// <summary>
	/// Gets or sets the desired response projection layout.
	/// </summary>
	public ResponseLayout? Layout { get; set; }
}
