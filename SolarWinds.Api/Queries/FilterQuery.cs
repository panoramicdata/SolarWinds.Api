using System.Collections.Generic;
using System.Linq;

namespace SolarWinds.Api.Queries;

public class FilterQuery<T> where T : Entity
{
	public string OrderBy { get; set; } = string.Empty;

	public int Skip { get; set; }

	public int Take { get; set; } = int.MaxValue;

	public List<Constraint> Constraints { get; set; } = [];

	public string ConstraintsString => Constraints == null || Constraints.Count == 0
		? string.Empty
		: " WHERE " + string.Join(" AND ", Constraints.Select(c => c.SqlSnippet));

	public string OrderByString => string.IsNullOrEmpty(OrderBy)
		? string.Empty
		: " ORDER BY " + OrderBy;

	private static IEnumerable<string> PropertyNames => typeof(T).GetProperties().Select(static p => p.Name);

	public SqlQuery GetSqlQuery()
	{
		var sql = $"SELECT {string.Join(", ", PropertyNames)} FROM {Entity.GetTableFullName<T>()}{ConstraintsString}{OrderByString} WITH ROWS @FirstRow TO @LastRow WITH TOTALROWS";
		var parameters = new Dictionary<string, object>
			{
				{"OrderBy", OrderBy },
				{"FirstRow", Skip + 1 },
				{"LastRow", Skip + Take },
			};

		return new SqlQuery
		{
			Sql = sql,
			Parameters = parameters
		};
	}
}