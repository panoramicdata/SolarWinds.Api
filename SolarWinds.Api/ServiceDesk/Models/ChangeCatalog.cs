using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk change catalog.
/// </summary>
public class ChangeCatalog : WorkItemEntity
{
	public string? Number { get; set; }
	public string? State { get; set; }
	public object? Site { get; set; }
	public object? Department { get; set; }
	public string? Priority { get; set; }
	public int? DefaultAssigneeId { get; set; }
	public string? ChangePlan { get; set; }
	public string? RollbackPlan { get; set; }
	public string? TestPlan { get; set; }
	public bool? ShowInPortal { get; set; }
}
