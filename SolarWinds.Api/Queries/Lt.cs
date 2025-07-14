namespace SolarWinds.Api.Queries;

public class Lt(string table, string value) : Constraint(table, value)
{
	public override string SqlSnippet => $"{Table}<{Value}";
}
