using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SolarWinds.Api.Queries
{
	/// <summary>
	/// A Query response
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[DataContract]
	public class QueryResponse<T>
	{
		/// <summary>
		/// The results
		/// </summary>
		[DataMember(Name = "results")]
		public List<T> Results { get; set; }

		/// <summary>
		/// The total number of rows that would be returned if no "WITH ROWS" was specified
		/// </summary>
		[DataMember(Name = "totalRows")]
		public int? TotalRows { get; set; }
	}
}
