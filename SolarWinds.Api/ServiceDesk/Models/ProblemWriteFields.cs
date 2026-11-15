namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable problem fields used by create and update operations.
/// </summary>
public sealed class ProblemWriteFields : ItsmRecordFields
{
	/// <summary>
	/// Gets or sets state value.
	/// </summary>
	public int? State { get; set; }

	/// <summary>
	/// Gets or sets root cause text.
	/// </summary>
	public string? RootCause { get; set; }

	/// <summary>
	/// Gets or sets symptoms text.
	/// </summary>
	public string? Symptoms { get; set; }

	/// <summary>
	/// Gets or sets workaround text.
	/// </summary>
	public string? Workaround { get; set; }

	/// <summary>
	/// Gets or sets linked incident identifiers.
	/// </summary>
	public int[]? IncidentIds { get; set; }

	/// <summary>
	/// Gets or sets linked ITSM change identifiers.
	/// </summary>
	public int[]? ItsmChangeIds { get; set; }
}
