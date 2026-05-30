using System.Text.Json.Serialization;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Strongly typed response for setup/itsm_states/init_states_data.json.
/// </summary>
public sealed class ItsmStatesInitStatesDataResponse
{
	[JsonPropertyName("states")]
	public List<ItsmStateDefinition> States { get; set; } = [];

	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

/// <summary>
/// Represents a single ITSM state definition.
/// </summary>
public sealed class ItsmStateDefinition
{
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	[JsonPropertyName("key")]
	public string? Key { get; set; }

	[JsonPropertyName("title")]
	public string? Title { get; set; }

	[JsonPropertyName("color")]
	public int? Color { get; set; }

	[JsonPropertyName("archived")]
	public bool? Archived { get; set; }

	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}
