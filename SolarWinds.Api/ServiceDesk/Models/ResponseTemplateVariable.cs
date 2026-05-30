using System.Text.Json.Serialization;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a response template variable for a specific incident.
/// </summary>
public class ResponseTemplateVariable
{
	/// <summary>
	/// Gets or sets the variable name/key.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the variable value.
	/// </summary>
	[JsonPropertyName("value")]
	public string? Value { get; set; }

	/// <summary>
	/// Gets or sets the variable label displayed in the UI.
	/// </summary>
	[JsonPropertyName("label")]
	public string? Label { get; set; }
}
