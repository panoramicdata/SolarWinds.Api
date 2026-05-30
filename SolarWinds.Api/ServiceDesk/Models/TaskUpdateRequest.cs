namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a task.
/// </summary>
public sealed class TaskUpdateRequest
{
	/// <summary>
	/// Gets or sets the task fields to update.
	/// </summary>
	public TaskWriteFields Task { get; set; } = new();
}
