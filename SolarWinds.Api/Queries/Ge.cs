namespace SolarWinds.Api.Queries;

/// <summary>
/// Greater-than-or-equal constraint (<c>&gt;=</c>) for SWQL filter expressions.
/// </summary>
/// <remarks>
/// Initializes a new greater-than-or-equal constraint.
/// </remarks>
/// <param name="table">Table column name.</param>
/// <param name="value">Value to compare.</param>
public class Ge(string table, string value) : Constraint(table, value)
{

	/// <summary>
	/// Gets the SWQL snippet for the greater-than-or-equal comparison.
	/// </summary>
	public override string SqlSnippet => $"{Table}>={Value}";
}
