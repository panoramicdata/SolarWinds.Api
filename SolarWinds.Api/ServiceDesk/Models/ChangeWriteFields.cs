namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable change fields used by create and update operations.
/// </summary>
public sealed class ChangeWriteFields : ItsmRecordFields
{
	/// <summary>
	/// Gets or sets change type identifier.
	/// </summary>
	public int? ChangeType { get; set; }

	/// <summary>
	/// Gets or sets state identifier.
	/// </summary>
	public int? StateId { get; set; }

	/// <summary>
	/// Gets or sets change plan text.
	/// </summary>
	public string? ChangePlan { get; set; }

	/// <summary>
	/// Gets or sets rollback plan text.
	/// </summary>
	public string? RollbackPlan { get; set; }

	/// <summary>
	/// Gets or sets test plan text.
	/// </summary>
	public string? TestPlan { get; set; }

	/// <summary>
	/// Gets or sets planned start date/time.
	/// </summary>
	public DateTime? PlannedStartAt { get; set; }

	/// <summary>
	/// Gets or sets planned end date/time.
	/// </summary>
	public DateTime? PlannedEndAt { get; set; }
}
