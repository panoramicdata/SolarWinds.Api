using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for listing software records.
/// </summary>
public sealed class GetSoftwaresRequest
{
	/// <summary>
	/// Response layout. Supported values are "short" and "long".
	/// </summary>
	[AliasAs("layout")]
	public ResponseLayout? Layout { get; set; }

	/// <summary>
	/// Optional report identifier used by saved/portal report-backed software searches.
	/// </summary>
	[AliasAs("report_id")]
	public int? ReportId { get; set; }

	/// <summary>
	/// Optional portal flag indicating filters are applied.
	/// </summary>
	[AliasAs("applied")]
	public bool? Applied { get; set; }

	/// <summary>
	/// Optional software category filters using the portal category[] parameter shape.
	/// </summary>
	[AliasAs("category[]")]
	public string[]? Category { get; set; }

	/// <summary>
	/// Optional software id filters using the portal software[] parameter shape.
	/// </summary>
	[AliasAs("software[]")]
	public int[]? Software { get; set; }

	/// <summary>
	/// Optional comma-delimited portal column list.
	/// </summary>
	[AliasAs("columns")]
	public string? Columns { get; set; }
}
