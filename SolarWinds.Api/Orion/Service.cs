using System.Runtime.Serialization;

namespace SolarWinds.Api.Orion;

/// <summary>
/// A Service
/// </summary>
[DataContract]
public class Service : Entity
{
	/// <summary>
	/// ObjectSubType
	/// </summary>
	[DataMember(Name = "ServiceName")]
	public string ServiceName { get; set; }

	/// <summary>
	/// Memory
	/// </summary>
	[DataMember(Name = "Memory")]
	public long Memory { get; set; }
}
