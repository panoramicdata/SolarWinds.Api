using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for setup/itsm_states/init_tabs_data.json.
/// </summary>
public sealed class GetItsmStatesInitTabsDataRequest
{
	[AliasAs("settings[custom_states_incident][visibilityKey]")]
	public string? CustomStatesIncidentVisibilityKey { get; set; }

	[AliasAs("settings[custom_states_incident][visibilityValue]")]
	public string? CustomStatesIncidentVisibilityValue { get; set; }

	[AliasAs("settings[custom_states_change][visibilityKey]")]
	public string? CustomStatesChangeVisibilityKey { get; set; }

	[AliasAs("settings[custom_states_change][visibilityValue]")]
	public string? CustomStatesChangeVisibilityValue { get; set; }

	[AliasAs("settings[custom_states_project][visibilityValue]")]
	public string? CustomStatesProjectVisibilityValue { get; set; }

	[AliasAs("settings[custom_states_asset][visibilityKey]")]
	public string? CustomStatesAssetVisibilityKey { get; set; }

	[AliasAs("settings[custom_states_asset][visibilityValue]")]
	public string? CustomStatesAssetVisibilityValue { get; set; }

	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}
