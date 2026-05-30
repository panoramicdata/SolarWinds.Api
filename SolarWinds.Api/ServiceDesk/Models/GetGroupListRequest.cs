using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for the groups list endpoint.
/// </summary>
public sealed class GetGroupListRequest
{
	/// <summary>
	/// Gets or sets the page number (1-based).
	/// </summary>
	[AliasAs("page")]
	public int Page { get; set; } = 1;

	/// <summary>
	/// Gets or sets the number of results per page.
	/// </summary>
	[AliasAs("per_page")]
	public int PerPage { get; set; } = 25;

	/// <summary>
	/// Gets or sets an optional name filter.
	/// </summary>
	[AliasAs("name")]
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets whether to filter to portal groups only.
	/// </summary>
	[AliasAs("filter_portal")]
	public bool FilterPortal { get; set; }

	/// <summary>
	/// Gets or sets whether to filter to queue groups only.
	/// </summary>
	[AliasAs("filter_queue")]
	public bool FilterQueue { get; set; }
}
