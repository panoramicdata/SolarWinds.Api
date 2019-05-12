namespace SolarWinds.Api.Queries
{
	public class Le : Constraint
	{
		public Le(string table, string value) : base(table, value)
		{
		}

		public override string SqlSnippet => $"{Table}<={Value}";
	}
}
