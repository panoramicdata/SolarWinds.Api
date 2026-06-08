using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for listing incidents.
/// </summary>
public sealed class GetIncidentsRequest
{
	/// <summary>
	/// Response layout. Supported values are "short" and "long".
	/// </summary>
	[AliasAs("layout")]
	public ResponseLayout Layout { get; set; }

	/// <summary>
	/// Updated-at filter selector (for example: "7", "24h", or "Select Date Range").
	/// </summary>
	[AliasAs("updated")]
	public string? Updated { get; set; }

	/// <summary>
	/// Updated-at custom range start used with Updated = "Select Date Range".
	/// </summary>
	[AliasAs("updated_custom_gte")]
	public DateTime? UpdatedCustomGte { get; set; }

	/// <summary>
	/// Updated-at custom range end used with Updated = "Select Date Range".
	/// </summary>
	[AliasAs("updated_custom_lte")]
	public DateTime? UpdatedCustomLte { get; set; }

	/// <summary>
	/// ISO timestamp for updated range start.
	/// </summary>
	[AliasAs("updated_from")]
	public DateTime? UpdatedFrom { get; set; }

	/// <summary>
	/// ISO timestamp for updated range end.
	/// </summary>
	[AliasAs("updated_to")]
	public DateTime? UpdatedTo { get; set; }

	/// <summary>
	/// ISO timestamp for created range start.
	/// </summary>
	[AliasAs("created_from")]
	public DateTime? CreatedFrom { get; set; }

	/// <summary>
	/// ISO timestamp for created range end.
	/// </summary>
	[AliasAs("created_to")]
	public DateTime? CreatedTo { get; set; }

	/// <summary>
	/// Optional 1-based page number.
	/// </summary>
	[AliasAs("page")]
	public int? Page { get; set; }

	/// <summary>
	/// Optional page size.
	/// </summary>
	[AliasAs("per_page")]
	public int? PerPage { get; set; }

	/// <summary>
	/// Optional report identifier used by saved/portal report-backed incident searches.
	/// </summary>
	[AliasAs("report_id")]
	public int? ReportId { get; set; }

	/// <summary>
	/// Optional context search term for portal-style incident search.
	/// </summary>
	[AliasAs("search_in_context")]
	public string? SearchInContext { get; set; }

	/// <summary>
	/// Optional request correlation value observed in portal requests.
	/// </summary>
	[AliasAs("requestId")]
	public string? RequestId { get; set; }

	/// <summary>
	/// Optional connection correlation value observed in portal requests.
	/// </summary>
	[AliasAs("connectionId")]
	public string? ConnectionId { get; set; }

	/// <summary>
	/// Optional portal flag indicating filters are applied.
	/// </summary>
	[AliasAs("applied")]
	public bool? Applied { get; set; }

	/// <summary>
	/// Optional description search terms using the portal description[] parameter shape.
	/// </summary>
	[AliasAs("description[]")]
	public string[]? Description { get; set; }

	/// <summary>
	/// Optional negated description terms using the portal description_is_not[] parameter shape.
	/// </summary>
	[AliasAs("description_is_not[]")]
	public string[]? DescriptionIsNot { get; set; }

	/// <summary>
	/// Optional department filters using the portal department[] parameter shape.
	/// </summary>
	[AliasAs("department[]")]
	public int[]? Department { get; set; }

	/// <summary>
	/// Optional title search terms using the portal title[] parameter shape.
	/// </summary>
	[AliasAs("title[]")]
	public string[]? Title { get; set; }


	/// <summary>
	/// Optional state search terms using the portal state[] parameter shape.
	/// </summary>
	[AliasAs("state[]")]
	public string[]? State { get; set; }

	/// <summary>
	/// Optional sort field (for example: "title").
	/// </summary>
	[AliasAs("sort_by")]
	public string? SortBy { get; set; }

	/// <summary>
	/// Optional sort direction (for example: "DESC").
	/// </summary>
	[AliasAs("sort_order")]
	public string? SortOrder { get; set; }

	/// <summary>
	/// Optional comma-delimited portal column list.
	/// </summary>
	[AliasAs("columns")]
	public string? Columns { get; set; }
}