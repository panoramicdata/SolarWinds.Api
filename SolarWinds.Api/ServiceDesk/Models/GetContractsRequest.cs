namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query options for contracts endpoints.
/// </summary>
public sealed class GetContractsRequest
{
	/// <summary>
	/// Gets or sets the desired response projection layout.
	/// </summary>
	public ResponseLayout? Layout { get; set; }
}
