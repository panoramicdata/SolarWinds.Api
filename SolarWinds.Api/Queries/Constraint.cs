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
			switch (value)
			{
				case string stringObject:
					Value = $"'{value}'";
					break;
				default:
					Value = $"{value}";
					break;
			}
		}

		public abstract string SqlSnippet { get; }
		public string Table { get; }
		public string Value { get; }
	}
}
