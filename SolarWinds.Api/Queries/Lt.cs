namespace SolarWinds.Api.Queries;

/// <summary>
/// Less-than constraint (<c>&lt;</c>) for SWQL filter expressions.
/// </summary>
/// <remarks>
/// Initializes a new less-than constraint.
/// </remarks>
/// <param name="table">Table column name.</param>
/// <param name="value">Value to compare.</param>
public class Lt(string table, string value) : Constraint(table, value)
{

	/// <summary>
	/// Gets the SWQL snippet for the less-than comparison.
	/// </summary>
	public override string SqlSnippet => $"{Table}<{Value}";
}
