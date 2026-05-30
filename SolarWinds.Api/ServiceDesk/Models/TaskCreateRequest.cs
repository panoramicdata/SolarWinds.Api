namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class TaskCreateRequest
{
	public TaskWriteFields Task { get; set; } = new();
}
