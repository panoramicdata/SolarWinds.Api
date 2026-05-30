namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Strongly typed response for setup/itsm_states/init_tabs_data.json.
/// </summary>
public sealed class ItsmStatesInitTabsDataResponse
{
	/// <summary>
	/// Gets or sets setup tabs returned by the ITSM states endpoint.
	/// </summary>
	[JsonPropertyName("tabs")]
	public List<ItsmStatesTab> Tabs { get; set; } = [];

	/// <summary>
	/// Gets or sets additional unmodeled response fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

/// <summary>
/// Represents a single setup ITSM tab payload.
/// </summary>
public sealed class ItsmStatesTab
{
	/// <summary>
	/// Gets or sets tab key.
	/// </summary>
	[JsonPropertyName("key")]
	public string? Key { get; set; }

	/// <summary>
	/// Gets or sets tab display title.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	/// <summary>
	/// Gets or sets whether the tab is visible.
	/// </summary>
	[JsonPropertyName("visible")]
	public bool? Visible { get; set; }

	/// <summary>
	/// Gets or sets additional unmodeled tab fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}
