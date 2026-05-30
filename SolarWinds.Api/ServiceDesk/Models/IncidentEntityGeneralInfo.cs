namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents the incident general info payload returned by entity_general_info.
/// </summary>
public class IncidentEntityGeneralInfo
{
	/// <summary>
	/// Gets or sets contextual help metadata for the incident page.
	/// </summary>
	[JsonPropertyName("help")]
	public IncidentEntityGeneralInfoHelp? Help { get; set; }

	/// <summary>
	/// Gets or sets the current signed-in user identifier.
	/// </summary>
	[JsonPropertyName("currentUserId")]
	public int CurrentUserId { get; set; }

	/// <summary>
	/// Gets or sets the description rendering mode used by the UI.
	/// </summary>
	[JsonPropertyName("descriptionsDisplayMode")]
	public int DescriptionsDisplayMode { get; set; }

	/// <summary>
	/// Gets or sets whether audit records are visible to the current user.
	/// </summary>
	[JsonPropertyName("canViewAudits")]
	public bool CanViewAudits { get; set; }

	/// <summary>
	/// Gets or sets secondary header text shown in the incident top bar.
	/// </summary>
	[JsonPropertyName("topBarSecondaryHeader")]
	public string? TopBarSecondaryHeader { get; set; }

	/// <summary>
	/// Gets or sets whether new comments should be shown first.
	/// </summary>
	[JsonPropertyName("newCommentFirst")]
	public bool NewCommentFirst { get; set; }

	/// <summary>
	/// Gets or sets the default tab key for incident detail pages.
	/// </summary>
	[JsonPropertyName("defaultTab")]
	public string? DefaultTab { get; set; }

	/// <summary>
	/// Gets or sets whether the incident is generally editable.
	/// </summary>
	[JsonPropertyName("updatable")]
	public bool Updatable { get; set; }

	/// <summary>
	/// Gets or sets whether state transitions are editable.
	/// </summary>
	[JsonPropertyName("updatableState")]
	public bool UpdatableState { get; set; }

	/// <summary>
	/// Gets or sets whether ability-related fields are editable.
	/// </summary>
	[JsonPropertyName("abilityUpdatable")]
	public bool AbilityUpdatable { get; set; }

	/// <summary>
	/// Gets or sets available workflow states for this incident.
	/// </summary>
	[JsonPropertyName("states")]
	public List<IncidentEntityGeneralInfoState> States { get; set; } = [];

	/// <summary>
	/// Gets or sets available UI actions for this incident.
	/// </summary>
	[JsonPropertyName("actions")]
	public List<IncidentEntityGeneralInfoAction> Actions { get; set; } = [];

	/// <summary>
	/// Gets or sets whether ad-hoc change creation is enabled.
	/// </summary>
	[JsonPropertyName("adHocChangeEnabled")]
	public bool AdHocChangeEnabled { get; set; }

	/// <summary>
	/// Gets or sets whether data masking is active for this incident.
	/// </summary>
	[JsonPropertyName("hasMasking")]
	public bool HasMasking { get; set; }

	/// <summary>
	/// Gets or sets object-type-specific metadata flags.
	/// </summary>
	[JsonPropertyName("objectTypeData")]
	public IncidentEntityGeneralInfoObjectTypeData? ObjectTypeData { get; set; }
}

/// <summary>
/// Represents help metadata for incident general info.
/// </summary>
public class IncidentEntityGeneralInfoHelp
{
	/// <summary>
	/// Gets or sets the controller name for the help target.
	/// </summary>
	[JsonPropertyName("controller")]
	public string? Controller { get; set; }

	/// <summary>
	/// Gets or sets the action name for the help target.
	/// </summary>
	[JsonPropertyName("action")]
	public string? Action { get; set; }

	/// <summary>
	/// Gets or sets the help item identifier.
	/// </summary>
	[JsonPropertyName("item")]
	public string? Item { get; set; }
}

/// <summary>
/// Represents an available incident state.
/// </summary>
public class IncidentEntityGeneralInfoState
{
	/// <summary>
	/// Gets or sets state identifier.
	/// </summary>
	[JsonPropertyName("id")]
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets state key.
	/// </summary>
	[JsonPropertyName("key")]
	public string? Key { get; set; }

	/// <summary>
	/// Gets or sets state title displayed in the UI.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	/// <summary>
	/// Gets or sets state color code.
	/// </summary>
	[JsonPropertyName("color")]
	public int Color { get; set; }

	/// <summary>
	/// Gets or sets whether this is the selected state.
	/// </summary>
	[JsonPropertyName("selected")]
	public bool Selected { get; set; }

	/// <summary>
	/// Gets or sets whether this state is archived.
	/// </summary>
	[JsonPropertyName("archived")]
	public bool Archived { get; set; }
}

/// <summary>
/// Represents an available incident action.
/// </summary>
public class IncidentEntityGeneralInfoAction
{
	/// <summary>
	/// Gets or sets action label displayed in the UI.
	/// </summary>
	[JsonPropertyName("label")]
	public string? Label { get; set; }

	/// <summary>
	/// Gets or sets action type value.
	/// </summary>
	[JsonPropertyName("type")]
	public string? Type { get; set; }

	/// <summary>
	/// Gets or sets action target link.
	/// </summary>
	[JsonPropertyName("link")]
	public string? Link { get; set; }

	/// <summary>
	/// Gets or sets action kind/category.
	/// </summary>
	[JsonPropertyName("kind")]
	public string? Kind { get; set; }

	/// <summary>
	/// Gets or sets whether the action intentionally omits an href.
	/// </summary>
	[JsonPropertyName("no_href")]
	public bool? NoHref { get; set; }
}

/// <summary>
/// Represents object-type-specific flags for incident general info.
/// </summary>
public class IncidentEntityGeneralInfoObjectTypeData
{
	/// <summary>
	/// Gets or sets whether incidents are allowed to transition to a resolved state.
	/// </summary>
	[JsonPropertyName("allowIncidentToResolve")]
	public bool AllowIncidentToResolve { get; set; }
}
