namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class TaskUpdateRequest
{
	public TaskWriteFields Task { get; set; } = new();
}
