using System.Runtime.Serialization;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Supported object types for object-scoped Service Desk routes.
/// </summary>
public enum ObjectType
{
	/// <summary>
	/// Incident records.
	/// </summary>
	[EnumMember(Value = "incidents")]
	Incidents,

	/// <summary>
	/// Problem records.
	/// </summary>
	[EnumMember(Value = "problems")]
	Problems,

	/// <summary>
	/// Change records.
	/// </summary>
	[EnumMember(Value = "changes")]
	Changes,

	/// <summary>
	/// Solution records.
	/// </summary>
	[EnumMember(Value = "solutions")]
	Solutions,

	/// <summary>
	/// Release records.
	/// </summary>
	[EnumMember(Value = "releases")]
	Releases,

	/// <summary>
	/// Purchase order records.
	/// </summary>
	[EnumMember(Value = "purchase_orders")]
	PurchaseOrders,

	/// <summary>
	/// Configuration item records.
	/// </summary>
	[EnumMember(Value = "configuration_items")]
	ConfigurationItems,

	/// <summary>
	/// Other asset records.
	/// </summary>
	[EnumMember(Value = "other_assets")]
	OtherAssets,

	/// <summary>
	/// Hardware asset records.
	/// </summary>
	[EnumMember(Value = "hardwares")]
	Hardwares,

	/// <summary>
	/// Mobile asset records.
	/// </summary>
	[EnumMember(Value = "mobiles")]
	Mobiles,

	/// <summary>
	/// Printer asset records.
	/// </summary>
	[EnumMember(Value = "printers")]
	Printers,

	/// <summary>
	/// Software asset records.
	/// </summary>
	[EnumMember(Value = "softwares")]
	Softwares,

	/// <summary>
	/// Contract records.
	/// </summary>
	[EnumMember(Value = "contracts")]
	Contracts,

	/// <summary>
	/// Ticket records.
	/// </summary>
	[EnumMember(Value = "tickets")]
	Tickets,

	/// <summary>
	/// User records.
	/// </summary>
	[EnumMember(Value = "users")]
	Users,

	/// <summary>
	/// Group records.
	/// </summary>
	[EnumMember(Value = "groups")]
	Groups,

	/// <summary>
	/// Department records.
	/// </summary>
	[EnumMember(Value = "departments")]
	Departments,

	/// <summary>
	/// Site records.
	/// </summary>
	[EnumMember(Value = "sites")]
	Sites,
}
