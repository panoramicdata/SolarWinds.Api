using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class UiCustomViewRequest
{
	[AliasAs("page_parameters[controller]")]
	public string? PageParametersController { get; set; }

	[AliasAs("page_parameters[action]")]
	public string? PageParametersAction { get; set; }

	[AliasAs("page_parameters[report_id]")]
	public int? PageParametersReportId { get; set; }

	[AliasAs("page_parameters[enabled]")]
	public int? PageParametersEnabled { get; set; }

	[AliasAs("page_parameters[sort_by]")]
	public string? PageParametersSortBy { get; set; }

	[AliasAs("page_parameters[sort_order]")]
	public string? PageParametersSortOrder { get; set; }

	[AliasAs("report_id")]
	public int? ReportId { get; set; }

	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

public sealed class UiCustomViewMetadataRequest
{
	[AliasAs("report_id")]
	public int? ReportId { get; set; }

	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

public sealed class UiCustomViewResponse
{
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

public sealed class UiCustomViewMetadataResponse
{
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}
