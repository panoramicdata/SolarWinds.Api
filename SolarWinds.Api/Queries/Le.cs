namespace SolarWinds.Api.Queries;

public class Le(string table, string value) : Constraint(table, value)
{
	public override string SqlSnippet => $"{Table}<={Value}";
}
