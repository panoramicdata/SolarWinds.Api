namespace SolarWinds.Api.Queries;

public class Eq(string table, object value) : Constraint(table, value)
{
	public override string SqlSnippet => $"{Table}={Value}";
}
