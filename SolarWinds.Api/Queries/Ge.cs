namespace SolarWinds.Api.Queries
{
	public class Ge : Constraint
	{
		public Ge(string table, string value) : base(table, value)
		{
		}

		public override string SqlSnippet => $"{Table}>={Value}";
	}
}
