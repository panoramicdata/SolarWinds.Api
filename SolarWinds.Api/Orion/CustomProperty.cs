using System.Text.Json.Serialization;

namespace SolarWinds.Api.Orion;

/// <summary>
/// A Custom Property
/// </summary>
[Table("Orion.CustomProperty")]
public class CustomProperty : Entity
{
	/// <summary>
	/// The table
	/// </summary>
	[JsonPropertyName("Table")]
	public required string Table { get; set; }

	/// <summary>
	/// The Field
	/// </summary>
	[JsonPropertyName("Field")]
	public required string Field { get; set; }

	/// <summary>
	/// The DataType
	/// </summary>
	[JsonPropertyName("DataType")]
	public required string DataType { get; set; }

	/// <summary>
	/// The max length
	/// </summary>
	[JsonPropertyName("MaxLength")]
	public int MaxLength { get; set; }

	/// <summary>
	/// The StorageMethod
	/// </summary>
	[JsonPropertyName("StorageMethod")]
	public required string StorageMethod { get; set; }

	/// <summary>
	/// The TargetEntity
	/// </summary>
	[JsonPropertyName("TargetEntity")]
	public required string TargetEntity { get; set; }

	/// <summary>
	/// Whether this is mandatory
	/// </summary>
	[JsonPropertyName("Mandatory")]
	public bool Mandatory { get; set; }

	/// <summary>
	/// The Default
	/// </summary>
	[JsonPropertyName("Default")]
	public required string Default { get; set; }
}
