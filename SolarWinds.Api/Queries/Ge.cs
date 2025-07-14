namespace SolarWinds.Api.Queries;

public class Ge(string table, string value) : Constraint(table, value)
{
	public override string SqlSnippet => $"{Table}>={Value}";
}
