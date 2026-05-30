using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for setup/itsm_states/init_states_data.json.
/// </summary>
public sealed class GetItsmStatesInitStatesDataRequest
{
	/// <summary>
	/// ITSM type value, for example: Incident.
	/// </summary>
	[AliasAs("itsm_type")]
	public string? ItsmType { get; set; }

	/// <summary>
	/// Whether the request is made in portal mode.
	/// </summary>
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}
