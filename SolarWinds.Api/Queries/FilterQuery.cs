namespace SolarWinds.Api.Queries;

/// <summary>
/// Builds a paged SWQL query from typed entity metadata and filter constraints.
/// </summary>
/// <typeparam name="T">Entity type being queried.</typeparam>
public class FilterQuery<T> where T : Entity
{
	/// <summary>
	/// Gets or sets the ORDER BY clause column expression.
	/// </summary>
	public string OrderBy { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the number of rows to skip.
	/// </summary>
	public int Skip { get; set; }

	/// <summary>
	/// Gets or sets the maximum number of rows to take.
	/// </summary>
	public int Take { get; set; } = int.MaxValue;

	/// <summary>
	/// Gets or sets the constraints combined into the WHERE clause.
	/// </summary>
	public List<Constraint> Constraints { get; set; } = [];

	/// <summary>
	/// Gets the rendered WHERE clause including the <c>WHERE</c> keyword when constraints exist.
	/// </summary>
	public string ConstraintsString => Constraints == null || Constraints.Count == 0
		? string.Empty
		: " WHERE " + string.Join(" AND ", Constraints.Select(c => c.SqlSnippet));

	/// <summary>
	/// Gets the rendered ORDER BY clause including the <c>ORDER BY</c> keyword when configured.
	/// </summary>
	public string OrderByString => string.IsNullOrEmpty(OrderBy)
		? string.Empty
		: " ORDER BY " + OrderBy;

	private static IEnumerable<string> PropertyNames => typeof(T).GetProperties().Select(static p => p.Name);

	/// <summary>
	/// Creates the SWQL query payload with paging parameters.
	/// </summary>
	/// <returns>A <see cref="SqlQuery"/> ready for SolarWinds query endpoints.</returns>
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