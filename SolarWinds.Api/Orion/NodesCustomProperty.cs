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
#pragma warning disable IDE1006 // Naming Styles
	public string? cp_dvcOwner { get; set; }
#pragma warning restore IDE1006 // Naming Styles

	/// <summary>
	/// The cp_dvcTypeTier custom property
	/// </summary>
	[DataMember(Name = "cp_dvcTypeTier")]
#pragma warning disable IDE1006 // Naming Styles
	public string? cp_dvcTypeTier { get; set; }
#pragma warning restore IDE1006 // Naming Styles
}
