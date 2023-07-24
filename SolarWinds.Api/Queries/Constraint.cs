namespace SolarWinds.Api.Queries
{
	/// <summary>
	/// A constraint
	/// </summary>
	public abstract class Constraint
	{
		protected Constraint(string table, object value)
		{
			Table = table;


			if(value is string stringObject)
			{
				Value = $"'{value}'";
			}
			else
			{
				Value = $"{value}";
			}
		}

		public abstract string SqlSnippet { get; }
		public string Table { get; }
		public string Value { get; }
	}
}
