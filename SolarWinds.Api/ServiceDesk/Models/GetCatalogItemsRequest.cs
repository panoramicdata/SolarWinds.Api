namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query options for catalog item endpoints.
/// </summary>
public sealed class GetCatalogItemsRequest
{
	/// <summary>
	/// Gets or sets the desired response projection layout.
	/// </summary>
	public ResponseLayout? Layout { get; set; }
}
