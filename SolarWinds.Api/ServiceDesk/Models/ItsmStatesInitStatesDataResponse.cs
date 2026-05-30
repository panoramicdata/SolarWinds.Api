namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Strongly typed response for setup/itsm_states/init_states_data.json.
/// </summary>
public sealed class ItsmStatesInitStatesDataResponse
{
	/// <summary>
	/// Gets or sets the set of ITSM state definitions.
	/// </summary>
	[JsonPropertyName("states")]
	public List<ItsmStateDefinition> States { get; set; } = [];

	/// <summary>
	/// Gets or sets additional unmodeled response fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

/// <summary>
/// Represents a single ITSM state definition.
/// </summary>
public sealed class ItsmStateDefinition
{
	/// <summary>
	/// Gets or sets state identifier.
	/// </summary>
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	/// <summary>
	/// Gets or sets stable state key.
	/// </summary>
	[JsonPropertyName("key")]
	public string? Key { get; set; }

	/// <summary>
	/// Gets or sets display title for the state.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	/// <summary>
	/// Gets or sets state color code.
	/// </summary>
	[JsonPropertyName("color")]
	public int? Color { get; set; }

	/// <summary>
	/// Gets or sets whether the state is archived.
	/// </summary>
	[JsonPropertyName("archived")]
	public bool? Archived { get; set; }

	/// <summary>
	/// Gets or sets additional unmodeled state fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}
