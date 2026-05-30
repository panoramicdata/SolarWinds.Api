namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query options for hardware endpoints.
/// </summary>
public sealed class GetHardwaresRequest
{
	/// <summary>
	/// Gets or sets the desired response projection layout.
	/// </summary>
	public ResponseLayout? Layout { get; set; }
}
