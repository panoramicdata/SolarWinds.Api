using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for listing users with UI/portal filters.
/// </summary>
public sealed class GetUsersRequest
{
	/// <summary>
	/// Gets or sets whether to filter by enabled state (1 for enabled, 0 for disabled).
	/// </summary>
	[AliasAs("enabled")]
	public int? Enabled { get; set; }

	/// <summary>
	/// Gets or sets the report identifier used by portal-backed user views.
	/// </summary>
	[AliasAs("report_id")]
	public int? ReportId { get; set; }

	/// <summary>
	/// Gets or sets whether the request should be evaluated in portal mode.
	/// </summary>
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}
