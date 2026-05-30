using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for listing users with UI/portal filters.
/// </summary>
public sealed class GetUsersRequest
{
	[AliasAs("enabled")]
	public int? Enabled { get; set; }

	[AliasAs("report_id")]
	public int? ReportId { get; set; }

	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}
