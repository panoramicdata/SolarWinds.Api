namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable solution fields used by create and update operations.
/// </summary>
public sealed class SolutionWriteFields : TaggedRecordFields
{
	/// <summary>
	/// Gets or sets solution state.
	/// </summary>
	public string? State { get; set; }

	/// <summary>
	/// Gets or sets solution category payload.
	/// </summary>
	public object? Category { get; set; }

	/// <summary>
	/// Gets or sets solution subcategory payload.
	/// </summary>
	public object? Subcategory { get; set; }

	/// <summary>
	/// Gets or sets linked incident identifiers.
	/// </summary>
	public int[]? IncidentIds { get; set; }
}
