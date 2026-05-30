namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query options for other assets endpoints.
/// </summary>
public sealed class GetOtherAssetsRequest
{
	/// <summary>
	/// Gets or sets the desired response projection layout.
	/// </summary>
	public ResponseLayout? Layout { get; set; }
}
