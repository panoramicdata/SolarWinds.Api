using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk incident.
/// </summary>
public class Incident : WorkItemEntity
{
	/// <summary>
	/// Gets or sets the incident number payload.
	/// </summary>
	public object? Number { get; set; }

	/// <summary>
	/// Gets or sets the incident state ID.
	/// </summary>
	public int StateId { get; set; }

	/// <summary>
	/// Gets or sets the incident state name.
	/// </summary>
	public string? State { get; set; }

	/// <summary>
	/// Gets or sets the incident priority.
	/// </summary>
	public string Priority { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the incident category.
	/// </summary>
	public object? Category { get; set; }

	/// <summary>
	/// Gets or sets the incident subcategory.
	/// </summary>
	public object? Subcategory { get; set; }

	/// <summary>
	/// Gets or sets the group assignee.
	/// </summary>
	public object? GroupAssignee { get; set; }

	/// <summary>
	/// Gets or sets the site.
	/// </summary>
	public object? Site { get; set; }

	/// <summary>
	/// Gets or sets the department.
	/// </summary>
	public object? Department { get; set; }

	/// <summary>
	/// Gets or sets the due date.
	/// </summary>
	public DateTime? DueAt { get; set; }

	/// <summary>
	/// Gets or sets the tag list.
	/// </summary>
	public string TagList { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the plain-text description (no HTML).
	/// </summary>
	public string? DescriptionNoHtml { get; set; }

	/// <summary>
	/// Gets or sets whether this is a service request.
	/// </summary>
	public bool IsServiceRequest { get; set; }

	/// <summary>
	/// Gets or sets the origin of the incident.
	/// </summary>
	public string? Origin { get; set; }

	/// <summary>
	/// Gets or sets the user who resolved the incident.
	/// </summary>
	public object? ResolvedBy { get; set; }

	/// <summary>
	/// Gets or sets the resolution description.
	/// </summary>
	public string? ResolutionDescription { get; set; }

	/// <summary>
	/// Gets or sets the resolution code.
	/// </summary>
	public string? ResolutionCode { get; set; }

	/// <summary>
	/// Gets or sets the CC list.
	/// </summary>
	public object[] Cc { get; set; } = [];

	/// <summary>
	/// Gets or sets the linked releases.
	/// </summary>
	public object[] Releases { get; set; } = [];

	/// <summary>
	/// Gets or sets the linked problems.
	/// </summary>
	public object[] Problems { get; set; } = [];

	/// <summary>
	/// Gets or sets the linked incidents.
	/// </summary>
	public object[] Incidents { get; set; } = [];

	/// <summary>
	/// Gets or sets the linked changes.
	/// </summary>
	public object[] Changes { get; set; } = [];

	/// <summary>
	/// Gets or sets the linked tasks.
	/// </summary>
	public object[] Tasks { get; set; } = [];

	/// <summary>
	/// Gets or sets the time tracks.
	/// </summary>
	public object[] TimeTracks { get; set; } = [];

	/// <summary>
	/// Gets or sets the linked solutions.
	/// </summary>
	public object[] Solutions { get; set; } = [];

	/// <summary>
	/// Gets or sets the linked assets.
	/// </summary>
	public object[] Assets { get; set; } = [];

	/// <summary>
	/// Gets or sets the linked mobile devices.
	/// </summary>
	public object[] Mobiles { get; set; } = [];

	/// <summary>
	/// Gets or sets the linked other assets.
	/// </summary>
	public object[] OtherAssets { get; set; } = [];

	/// <summary>
	/// Gets or sets the linked configuration items.
	/// </summary>
	public object[] ConfigurationItems { get; set; } = [];

	/// <summary>
	/// Gets or sets the linked discovery hardware.
	/// </summary>
	public object[] DiscoveryHardwares { get; set; } = [];

	/// <summary>
	/// Gets or sets the linked purchase orders.
	/// </summary>
	public object[] PurchaseOrders { get; set; } = [];
}
