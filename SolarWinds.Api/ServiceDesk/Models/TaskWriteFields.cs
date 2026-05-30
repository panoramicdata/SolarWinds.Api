namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class TaskWriteFields
{
	public string? Name { get; set; }
	public object? Assignee { get; set; }
	public DateTime? DueAt { get; set; }
	public bool? IsComplete { get; set; }
}
