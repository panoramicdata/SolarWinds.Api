namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable release fields used by create and update operations.
/// </summary>
public sealed class ReleaseWriteFields : ItsmRecordFields
{
	/// <summary>
	/// Gets or sets state value.
	/// </summary>
	public int? State { get; set; }

	/// <summary>
	/// Gets or sets release plan text.
	/// </summary>
	public string? Plan { get; set; }

	/// <summary>
	/// Gets or sets build text.
	/// </summary>
	public string? Build { get; set; }

	/// <summary>
	/// Gets or sets deploy text.
	/// </summary>
	public string? Deploy { get; set; }

	/// <summary>
	/// Gets or sets planned start date/time.
	/// </summary>
	public DateTime? PlannedStartAt { get; set; }

	/// <summary>
	/// Gets or sets planned end date/time.
	/// </summary>
	public DateTime? PlannedEndAt { get; set; }

	/// <summary>
	/// Gets or sets approval level attribute payloads.
	/// </summary>
	public object[]? ApprovalLevelsAttributes { get; set; }

	/// <summary>
	/// Gets or sets linked ITSM change identifiers.
	/// </summary>
	public int[]? ItsmChangeIds { get; set; }
}
