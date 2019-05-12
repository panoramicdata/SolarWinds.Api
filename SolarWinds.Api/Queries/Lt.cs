namespace SolarWinds.Api.Queries
{
	public class Lt : Constraint
	{
		public Lt(string table, string value) : base(table, value)
		{
		}

		public override string SqlSnippet => $"{Table}<{Value}";
	}
}
