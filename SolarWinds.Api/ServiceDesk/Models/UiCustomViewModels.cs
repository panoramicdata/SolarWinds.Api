using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for custom-view data endpoints.
/// </summary>
public sealed class UiCustomViewRequest
{
	/// <summary>
	/// Gets or sets page controller context.
	/// </summary>
	[AliasAs("page_parameters[controller]")]
	public string? PageParametersController { get; set; }

	/// <summary>
	/// Gets or sets page action context.
	/// </summary>
	[AliasAs("page_parameters[action]")]
	public string? PageParametersAction { get; set; }

	/// <summary>
	/// Gets or sets page report identifier.
	/// </summary>
	[AliasAs("page_parameters[report_id]")]
	public int? PageParametersReportId { get; set; }

	/// <summary>
	/// Gets or sets page enabled-state flag.
	/// </summary>
	[AliasAs("page_parameters[enabled]")]
	public int? PageParametersEnabled { get; set; }

	/// <summary>
	/// Gets or sets page sort column.
	/// </summary>
	[AliasAs("page_parameters[sort_by]")]
	public string? PageParametersSortBy { get; set; }

	/// <summary>
	/// Gets or sets page sort direction.
	/// </summary>
	[AliasAs("page_parameters[sort_order]")]
	public string? PageParametersSortOrder { get; set; }

	/// <summary>
	/// Gets or sets top-level report identifier.
	/// </summary>
	[AliasAs("report_id")]
	public int? ReportId { get; set; }

	/// <summary>
	/// Gets or sets whether to evaluate this query in portal mode.
	/// </summary>
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

/// <summary>
/// Query parameters for custom-view metadata endpoints.
/// </summary>
public sealed class UiCustomViewMetadataRequest
{
	/// <summary>
	/// Gets or sets report identifier.
	/// </summary>
	[AliasAs("report_id")]
	public int? ReportId { get; set; }

	/// <summary>
	/// Gets or sets whether to evaluate this query in portal mode.
	/// </summary>
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

/// <summary>
/// Dynamic response wrapper for custom-view data.
/// </summary>
public sealed class UiCustomViewResponse
{
	/// <summary>
	/// Gets or sets additional unmodeled response fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

/// <summary>
/// Dynamic response wrapper for custom-view metadata.
/// </summary>
public sealed class UiCustomViewMetadataResponse
{
	/// <summary>
	/// Gets or sets additional unmodeled response fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}
