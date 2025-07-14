using System.Runtime.Serialization;

namespace SolarWinds.Api.Orion;

/// <summary>
/// A Poller
/// </summary>
[DataContract]
public class Poller : Entity
{
	/// <summary>
	/// The PollerID
	/// </summary>
	[DataMember(Name = "PollerID")]
	public int PollerID { get; set; }

	/// <summary>
	/// The PollerType
	/// </summary>
	[DataMember(Name = "PollerType")]
	public string PollerType { get; set; }

	/// <summary>
	/// The NetObject
	/// </summary>
	[DataMember(Name = "NetObject")]
	public string NetObject { get; set; }

	/// <summary>
	/// The NetObjectType
	/// </summary>
	[DataMember(Name = "NetObjectType")]
	public string NetObjectType { get; set; }

	/// <summary>
	/// The NetObjectID
	/// </summary>
	[DataMember(Name = "NetObjectID")]
	public string NetObjectID { get; set; }

	/// <summary>
	/// Whether it is enabled
	/// </summary>
	[DataMember(Name = "Enabled")]
	public string Enabled { get; set; }
}
