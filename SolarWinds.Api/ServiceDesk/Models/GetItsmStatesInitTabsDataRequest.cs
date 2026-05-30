using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for setup/itsm_states/init_tabs_data.json.
/// </summary>
public sealed class GetItsmStatesInitTabsDataRequest
{
	/// <summary>
	/// Gets or sets the visibility key for incident custom states.
	/// </summary>
	[AliasAs("settings[custom_states_incident][visibilityKey]")]
	public string? CustomStatesIncidentVisibilityKey { get; set; }

	/// <summary>
	/// Gets or sets the visibility value for incident custom states.
	/// </summary>
	[AliasAs("settings[custom_states_incident][visibilityValue]")]
	public string? CustomStatesIncidentVisibilityValue { get; set; }

	/// <summary>
	/// Gets or sets the visibility key for change custom states.
	/// </summary>
	[AliasAs("settings[custom_states_change][visibilityKey]")]
	public string? CustomStatesChangeVisibilityKey { get; set; }

	/// <summary>
	/// Gets or sets the visibility value for change custom states.
	/// </summary>
	[AliasAs("settings[custom_states_change][visibilityValue]")]
	public string? CustomStatesChangeVisibilityValue { get; set; }

	/// <summary>
	/// Gets or sets the visibility value for project custom states.
	/// </summary>
	[AliasAs("settings[custom_states_project][visibilityValue]")]
	public string? CustomStatesProjectVisibilityValue { get; set; }

	/// <summary>
	/// Gets or sets the visibility key for asset custom states.
	/// </summary>
	[AliasAs("settings[custom_states_asset][visibilityKey]")]
	public string? CustomStatesAssetVisibilityKey { get; set; }

	/// <summary>
	/// Gets or sets the visibility value for asset custom states.
	/// </summary>
	[AliasAs("settings[custom_states_asset][visibilityValue]")]
	public string? CustomStatesAssetVisibilityValue { get; set; }

	/// <summary>
	/// Gets or sets whether to evaluate this request in portal mode.
	/// </summary>
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}
