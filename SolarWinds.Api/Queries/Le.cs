namespace SolarWinds.Api.Queries;

/// <summary>
/// Less-than-or-equal constraint (<c>&lt;=</c>) for SWQL filter expressions.
/// </summary>
/// <remarks>
/// Initializes a new less-than-or-equal constraint.
/// </remarks>
/// <param name="table">Table column name.</param>
/// <param name="value">Value to compare.</param>
public class Le(string table, string value) : Constraint(table, value)
{

	/// <summary>
	/// Gets the SWQL snippet for the less-than-or-equal comparison.
	/// </summary>
	public override string SqlSnippet => $"{Table}<={Value}";
}
