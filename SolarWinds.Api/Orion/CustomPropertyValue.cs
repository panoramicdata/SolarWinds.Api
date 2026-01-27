using System.Diagnostics;
using System.Runtime.Serialization;

namespace SolarWinds.Api.Orion;

/// <summary>
/// A CustomPropertyValue
/// </summary>
[DataContract]
[Table("Orion.CustomPropertyValues")]
[DebuggerDisplay("{Table}.{Field}={Value}")]
public class CustomPropertyValue : Entity
{
	/// <summary>
	/// The Table
	/// </summary>
	[DataMember(Name = "Table")]
	public required string Table { get; set; }

	/// <summary>
	/// The Field
	/// </summary>
	[DataMember(Name = "Field")]
	public required string Field { get; set; }

	/// <summary>
	/// The Value
	/// </summary>
	[DataMember(Name = "Value")]
	public required string Value { get; set; }
}
