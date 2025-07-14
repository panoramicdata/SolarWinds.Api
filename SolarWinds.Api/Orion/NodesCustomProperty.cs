using System.Runtime.Serialization;

namespace SolarWinds.Api.Orion;

/// <summary>
/// A Poller
/// </summary>
[DataContract]
[Table("Orion.NodesCustomProperties")]
public class NodesCustomProperty : CustomPropertiesEntity
{
	/// <summary>
	/// The NodeId
	/// </summary>
	[DataMember(Name = "NodeID")]
	public string NodeId { get; set; }
}
