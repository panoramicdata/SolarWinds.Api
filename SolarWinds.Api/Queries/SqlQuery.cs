using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SolarWinds.Api.Queries;

/// <summary>
/// A SQL query
/// </summary>
[DataContract]
public class SqlQuery
{
	/// <summary>
	/// The query string
	/// </summary>
	/// <example>SELECT * FROM Orion.Pollers WHERE PollerID>@p ORDER BY PollerID WITH ROWS 1 TO 3 WITH TOTALROWS</example>
	[DataMember(Name = "query")]
	public string Sql { get; set; } = string.Empty;

	/// <summary>
	/// The query parameters
	/// </summary>
	/// <example>p : 0</example>
	[DataMember(Name = "parameters")]
	public Dictionary<string, object> Parameters { get; set; } = [];
}
