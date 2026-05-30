namespace SolarWinds.Api.Queries;

/// <summary>
/// Represents a single WHERE-clause constraint in a SWQL query.
/// </summary>
public abstract class Constraint
{
	/// <summary>
	/// Initializes a new constraint for a table column and value.
	/// </summary>
	/// <param name="table">Table column name to constrain.</param>
	/// <param name="value">Value to compare against in SWQL.</param>
	protected Constraint(string table, object value)
	{
		Table = table;


		if(value is string stringObject)
		{
			Value = $"'{value}'";
		}
		else
		{
			Value = $"{value}";
		}
	}

	/// <summary>
	/// Gets the rendered SWQL constraint snippet (for example, <c>Name='Router'</c>).
	/// </summary>
	public abstract string SqlSnippet { get; }

	/// <summary>
	/// Gets the constrained table column name.
	/// </summary>
	public string Table { get; }

	/// <summary>
	/// Gets the serialized SWQL comparison value.
	/// </summary>
	public string Value { get; }
}
