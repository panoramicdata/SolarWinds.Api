namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Strongly typed response for setup/itsm_states/init_tabs_data.json.
/// </summary>
public sealed class ItsmStatesInitTabsDataResponse
{
	[JsonPropertyName("tabs")]
	public List<ItsmStatesTab> Tabs { get; set; } = [];

	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

/// <summary>
/// Represents a single setup ITSM tab payload.
/// </summary>
public sealed class ItsmStatesTab
{
	[JsonPropertyName("key")]
	public string? Key { get; set; }

	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("visible")]
	public bool? Visible { get; set; }

	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}
