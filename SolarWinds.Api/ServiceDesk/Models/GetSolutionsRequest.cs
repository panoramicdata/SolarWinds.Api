using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for listing and fetching solutions.
/// </summary>
public sealed class GetSolutionsRequest
{
	/// <summary>
	/// Response layout.
	/// </summary>
	[AliasAs("layout")]
	public ResponseLayout Layout { get; set; } = ResponseLayout.Short;
}
