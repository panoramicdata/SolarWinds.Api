namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable change request fields for change request creation.
/// </summary>
public sealed class ChangeRequestWriteFields
{
	/// <summary>
	/// Gets or sets request title.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets request description.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets change plan text.
	/// </summary>
	public string? ChangePlan { get; set; }

	/// <summary>
	/// Gets or sets test plan text.
	/// </summary>
	public string? TestPlan { get; set; }

	/// <summary>
	/// Gets or sets rollback plan text.
	/// </summary>
	public string? RollbackPlan { get; set; }

	/// <summary>
	/// Gets or sets priority value.
	/// </summary>
	public string? Priority { get; set; }

	/// <summary>
	/// Gets or sets change type value.
	/// </summary>
	public string? ChangeType { get; set; }

	/// <summary>
	/// Gets or sets assignee payload.
	/// </summary>
	public object? Assignee { get; set; }

	/// <summary>
	/// Gets or sets group assignee payload.
	/// </summary>
	public object? GroupAssignee { get; set; }

	/// <summary>
	/// Gets or sets site payload.
	/// </summary>
	public object? Site { get; set; }

	/// <summary>
	/// Gets or sets department payload.
	/// </summary>
	public object? Department { get; set; }

	/// <summary>
	/// Gets or sets request variable attribute payload.
	/// </summary>
	public object[]? RequestVariablesAttributes { get; set; }
}
