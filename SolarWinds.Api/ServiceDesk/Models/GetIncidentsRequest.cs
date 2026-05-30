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
}