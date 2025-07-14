namespace SolarWinds.Api.Queries;

public class Gt(string table, string value) : Constraint(table, value)
{
	public override string SqlSnippet => $"{Table}>{Value}";
}
