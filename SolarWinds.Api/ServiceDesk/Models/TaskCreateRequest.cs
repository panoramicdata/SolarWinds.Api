namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a task.
/// </summary>
public sealed class TaskCreateRequest
{
	/// <summary>
	/// Gets or sets the task payload to create.
	/// </summary>
	public TaskWriteFields Task { get; set; } = new();
}
