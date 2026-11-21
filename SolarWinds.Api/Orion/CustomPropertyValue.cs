using System.Diagnostics;
using System.Text.Json.Serialization;

namespace SolarWinds.Api.Orion;

/// <summary>
/// A CustomPropertyValue
/// </summary>
[Table("Orion.CustomPropertyValues")]
[DebuggerDisplay("{Table}.{Field}={Value}")]
public class CustomPropertyValue : Entity
{
	/// <summary>
	/// The Table
	/// </summary>
	[JsonPropertyName("Table")]
	public required string Table { get; set; }

	/// <summary>
	/// The Field
	/// </summary>
	[JsonPropertyName("Field")]
	public required string Field { get; set; }

	/// <summary>
	/// The Value
	/// </summary>
	[JsonPropertyName("Value")]
	public required string Value { get; set; }
}
