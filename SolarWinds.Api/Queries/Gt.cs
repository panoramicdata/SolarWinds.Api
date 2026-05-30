namespace SolarWinds.Api.Queries;

/// <summary>
/// Greater-than constraint (<c>&gt;</c>) for SWQL filter expressions.
/// </summary>
/// <remarks>
/// Initializes a new greater-than constraint.
/// </remarks>
/// <param name="table">Table column name.</param>
/// <param name="value">Value to compare.</param>
public class Gt(string table, string value) : Constraint(table, value)
{

	/// <summary>
	/// Gets the SWQL snippet for the greater-than comparison.
	/// </summary>
	public override string SqlSnippet => $"{Table}>{Value}";
}
