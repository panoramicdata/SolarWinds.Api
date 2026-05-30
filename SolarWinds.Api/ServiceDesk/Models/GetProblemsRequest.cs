using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for listing and fetching problems.
/// </summary>
public sealed class GetProblemsRequest
{
	/// <summary>
	/// Response layout.
	/// </summary>
	[AliasAs("layout")]
	public ResponseLayout Layout { get; set; } = ResponseLayout.Short;
}
