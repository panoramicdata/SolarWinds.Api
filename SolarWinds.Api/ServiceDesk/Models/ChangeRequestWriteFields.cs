namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable change request fields for change request creation.
/// </summary>
public sealed class ChangeRequestWriteFields
{
	public string? Name { get; set; }
	public string? Description { get; set; }
	public string? ChangePlan { get; set; }
	public string? TestPlan { get; set; }
	public string? RollbackPlan { get; set; }
	public string? Priority { get; set; }
	public string? ChangeType { get; set; }
	public object? Assignee { get; set; }
	public object? GroupAssignee { get; set; }
	public object? Site { get; set; }
	public object? Department { get; set; }
	public object[]? RequestVariablesAttributes { get; set; }
}
