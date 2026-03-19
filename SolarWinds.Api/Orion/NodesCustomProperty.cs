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
	public required string NodeId { get; set; }

	/// <summary>
	/// The cp_dvcOwner custom property
	/// </summary>
	[DataMember(Name = "cp_dvcOwner")]
	public string? cp_dvcOwner { get; set; }

	/// <summary>
	/// The cp_dvcTypeTier custom property
	/// </summary>
	[DataMember(Name = "cp_dvcTypeTier")]
	public string? cp_dvcTypeTier { get; set; }
}
