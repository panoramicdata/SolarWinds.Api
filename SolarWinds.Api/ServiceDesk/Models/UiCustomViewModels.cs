using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class UiCustomViewRequest
{
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
