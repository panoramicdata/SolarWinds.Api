namespace SolarWinds.Api.Queries
{
	public class Gt : Constraint
	{
		public Gt(string table, string value) : base(table, value)
		{
		}

		public override string SqlSnippet => $"{Table}>{Value}";
	}
}
