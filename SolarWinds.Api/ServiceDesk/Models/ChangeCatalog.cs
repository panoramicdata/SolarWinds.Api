using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk change catalog.
/// </summary>
public class ChangeCatalog : WorkItemEntity
{
	/// <summary>
	/// Gets or sets the catalog number.
	/// </summary>
	public string? Number { get; set; }

	/// <summary>
	/// Gets or sets the current catalog state name.
	/// </summary>
	public string? State { get; set; }

	/// <summary>
	/// Gets or sets site payload associated with this catalog.
	/// </summary>
	public object? Site { get; set; }

	/// <summary>
	/// Gets or sets department payload associated with this catalog.
	/// </summary>
	public object? Department { get; set; }

	/// <summary>
	/// Gets or sets default priority for requests created from this catalog.
	/// </summary>
	public string? Priority { get; set; }

	/// <summary>
	/// Gets or sets the default assignee identifier.
	/// </summary>
	public int? DefaultAssigneeId { get; set; }

	/// <summary>
	/// Gets or sets the default change plan text.
	/// </summary>
	public string? ChangePlan { get; set; }

	/// <summary>
	/// Gets or sets the default rollback plan text.
	/// </summary>
	public string? RollbackPlan { get; set; }

	/// <summary>
	/// Gets or sets the default test plan text.
	/// </summary>
	public string? TestPlan { get; set; }

	/// <summary>
	/// Gets or sets whether this catalog is available in the self-service portal.
	/// </summary>
	public bool? ShowInPortal { get; set; }
}
