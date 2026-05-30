namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query options for categories endpoints.
/// </summary>
public sealed class GetCategoriesRequest
{
	/// <summary>
	/// Gets or sets the desired response projection layout.
	/// </summary>
	public ResponseLayout? Layout { get; set; }
}
