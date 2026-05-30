namespace SolarWinds.Api.Queries;

/// <summary>
/// Not-equal constraint (<c>&lt;&gt;</c>) for SWQL filter expressions.
/// </summary>
/// <remarks>
/// Initializes a new not-equal constraint.
/// </remarks>
/// <param name="table">Table column name.</param>
/// <param name="value">Value to compare.</param>
public class Ne(string table, string value) : Constraint(table, value)
{

	/// <summary>
	/// Gets the SWQL snippet for the not-equal comparison.
	/// </summary>
	public override string SqlSnippet => $"{Table}<>{Value}";
}
