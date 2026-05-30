using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for listing and fetching changes.
/// </summary>
public sealed class GetChangesRequest
{
	/// <summary>
	/// Response layout.
	/// </summary>
	[AliasAs("layout")]
	public ResponseLayout Layout { get; set; } = ResponseLayout.Short;
}
