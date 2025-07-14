using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SolarWinds.Api.Orion;

/// <summary>
/// A Managed entity
/// </summary>
[DataContract]
public abstract class ManagedEntity : Entity
{
	/// <summary>
	/// Status
	/// </summary>
	[DataMember(Name = "Status")]
	public int Status { get; set; }

	/// <summary>
	/// StatusLED
	/// </summary>
	[DataMember(Name = "StatusLED")]
	public string StatusLED { get; set; }

	/// <summary>
	/// StatusDescription
	/// </summary>
	[DataMember(Name = "StatusDescription")]
	public string StatusDescription { get; set; }

	/// <summary>
	/// UnManaged
	/// </summary>
	[DataMember(Name = "UnManaged")]
	public bool UnManaged { get; set; }

	/// <summary>
	/// UnManageFrom
	/// </summary>
	[DataMember(Name = "UnManageFrom")]
	public DateTime UnManageFrom { get; set; }

	/// <summary>
	/// UnManageUntil
	/// </summary>
	[DataMember(Name = "UnManageUntil")]
	public DateTime UnManageUntil { get; set; }

	/// <summary>
	/// Image
	/// </summary>
	[DataMember(Name = "Image")]
	public string Image { get; set; }

	/// <summary>
	/// AncestorDisplayNames
	/// </summary>
	[DataMember(Name = "AncestorDisplayNames")]
	public List<string> AncestorDisplayNames { get; set; }

	/// <summary>
	/// AncestorDetailsUrls
	/// </summary>
	[DataMember(Name = "AncestorDetailsUrls")]
	public List<string> AncestorDetailsUrls { get; set; }

	/// <summary>
	/// StatusIconHint
	/// </summary>
	[DataMember(Name = "StatusIconHint")]
	public string StatusIconHint { get; set; }
}