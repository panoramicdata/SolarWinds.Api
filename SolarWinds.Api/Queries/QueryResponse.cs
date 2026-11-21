using System.Text.Json.Serialization;

namespace SolarWinds.Api.Queries;

/// <summary>
/// A Query response
/// </summary>
/// <typeparam name="T"></typeparam>
public class QueryResponse<T>
{
	/// <summary>
	/// The results
	/// </summary>
	[JsonPropertyName("results")]
	public List<T> Results { get; set; } = [];

	/// <summary>
	/// The total number of rows that would be returned if no "WITH ROWS" was specified
	/// </summary>
	[JsonPropertyName("totalRows")]
	public int? TotalRows { get; set; }
}
