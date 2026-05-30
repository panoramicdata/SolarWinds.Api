using System.Runtime.Serialization;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Supported object types for object-scoped Service Desk routes.
/// </summary>
public enum ObjectType
{
	[EnumMember(Value = "incidents")]
	Incidents,

	[EnumMember(Value = "problems")]
	Problems,

	[EnumMember(Value = "changes")]
	Changes,

	[EnumMember(Value = "solutions")]
	Solutions,

	[EnumMember(Value = "releases")]
	Releases,

	[EnumMember(Value = "purchase_orders")]
	PurchaseOrders,

	[EnumMember(Value = "configuration_items")]
	ConfigurationItems,

	[EnumMember(Value = "other_assets")]
	OtherAssets,

	[EnumMember(Value = "hardwares")]
	Hardwares,

	[EnumMember(Value = "mobiles")]
	Mobiles,

	[EnumMember(Value = "printers")]
	Printers,

	[EnumMember(Value = "softwares")]
	Softwares,

	[EnumMember(Value = "contracts")]
	Contracts,

	[EnumMember(Value = "tickets")]
	Tickets,

	[EnumMember(Value = "users")]
	Users,

	[EnumMember(Value = "groups")]
	Groups,

	[EnumMember(Value = "departments")]
	Departments,

	[EnumMember(Value = "sites")]
	Sites,
}
