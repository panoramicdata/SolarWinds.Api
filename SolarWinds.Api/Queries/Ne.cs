namespace SolarWinds.Api.Queries
{
	public class Ne : Constraint
	{
		public Ne(string table, string value) : base(table, value)
		{
		}

		public override string SqlSnippet => $"{Table}<>{Value}";
	}
}
