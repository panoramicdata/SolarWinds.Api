using System.Text.Json.Serialization;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents the incident general info payload returned by entity_general_info.
/// </summary>
public class IncidentEntityGeneralInfo
{
	[JsonPropertyName("help")]
	public IncidentEntityGeneralInfoHelp? Help { get; set; }

	[JsonPropertyName("currentUserId")]
	public int CurrentUserId { get; set; }

	[JsonPropertyName("descriptionsDisplayMode")]
	public int DescriptionsDisplayMode { get; set; }

	[JsonPropertyName("canViewAudits")]
	public bool CanViewAudits { get; set; }

	[JsonPropertyName("topBarSecondaryHeader")]
	public string? TopBarSecondaryHeader { get; set; }

	[JsonPropertyName("newCommentFirst")]
	public bool NewCommentFirst { get; set; }

	[JsonPropertyName("defaultTab")]
	public string? DefaultTab { get; set; }

	[JsonPropertyName("updatable")]
	public bool Updatable { get; set; }

	[JsonPropertyName("updatableState")]
	public bool UpdatableState { get; set; }

	[JsonPropertyName("abilityUpdatable")]
	public bool AbilityUpdatable { get; set; }

	[JsonPropertyName("states")]
	public List<IncidentEntityGeneralInfoState> States { get; set; } = [];

	[JsonPropertyName("actions")]
	public List<IncidentEntityGeneralInfoAction> Actions { get; set; } = [];

	[JsonPropertyName("adHocChangeEnabled")]
	public bool AdHocChangeEnabled { get; set; }

	[JsonPropertyName("hasMasking")]
	public bool HasMasking { get; set; }

	[JsonPropertyName("objectTypeData")]
	public IncidentEntityGeneralInfoObjectTypeData? ObjectTypeData { get; set; }
}

/// <summary>
/// Represents help metadata for incident general info.
/// </summary>
public class IncidentEntityGeneralInfoHelp
{
	[JsonPropertyName("controller")]
	public string? Controller { get; set; }

	[JsonPropertyName("action")]
	public string? Action { get; set; }

	[JsonPropertyName("item")]
	public string? Item { get; set; }
}

/// <summary>
/// Represents an available incident state.
/// </summary>
public class IncidentEntityGeneralInfoState
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("key")]
	public string? Key { get; set; }

	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("color")]
	public int Color { get; set; }

	[JsonPropertyName("selected")]
	public bool Selected { get; set; }

	[JsonPropertyName("archived")]
	public bool Archived { get; set; }
}

/// <summary>
/// Represents an available incident action.
/// </summary>
public class IncidentEntityGeneralInfoAction
{
	[JsonPropertyName("label")]
	public string? Label { get; set; }

	[JsonPropertyName("type")]
	public string? Type { get; set; }

	[JsonPropertyName("link")]
	public string? Link { get; set; }

	[JsonPropertyName("kind")]
	public string? Kind { get; set; }

	[JsonPropertyName("no_href")]
	public bool? NoHref { get; set; }
}

/// <summary>
/// Represents object-type-specific flags for incident general info.
/// </summary>
public class IncidentEntityGeneralInfoObjectTypeData
{
	[JsonPropertyName("allowIncidentToResolve")]
	public bool AllowIncidentToResolve { get; set; }
}
