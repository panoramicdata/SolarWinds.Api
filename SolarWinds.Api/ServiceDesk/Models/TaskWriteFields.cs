namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable task fields used by create and update operations.
/// </summary>
public sealed class TaskWriteFields
{
	/// <summary>
	/// Gets or sets task title.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets assignee payload.
	/// </summary>
	public object? Assignee { get; set; }

	/// <summary>
	/// Gets or sets task due date/time.
	/// </summary>
	public DateTime? DueAt { get; set; }

	/// <summary>
	/// Gets or sets whether the task is complete.
	/// </summary>
	public bool? IsComplete { get; set; }
}
