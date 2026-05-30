namespace SolarWinds.Api.Queries;

/// <summary>
/// Equality constraint (<c>=</c>) for SWQL filter expressions.
/// </summary>
/// <remarks>
/// Initializes a new equality constraint.
/// </remarks>
/// <param name="table">Table column name.</param>
/// <param name="value">Value to compare.</param>
public class Eq(string table, object value) : Constraint(table, value)
{

	/// <summary>
	/// Gets the SWQL snippet for the equality comparison.
	/// </summary>
	public override string SqlSnippet => $"{Table}={Value}";
}
