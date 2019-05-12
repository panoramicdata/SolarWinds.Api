namespace SolarWinds.Api.Queries
{
	public class Eq : Constraint
	{
		public Eq(string table, object value) : base(table, value)
		{
		}

		public override string SqlSnippet => $"{Table}={Value}";
	}
}
