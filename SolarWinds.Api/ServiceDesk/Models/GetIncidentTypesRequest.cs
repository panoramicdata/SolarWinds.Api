using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for listing incident types.
/// </summary>
public sealed class GetIncidentTypesRequest
{
	/// <summary>
	/// Gets or sets the parent type ID to filter by.
	/// </summary>
	[AliasAs("parent_id")]
	public int? ParentId { get; set; }

	/// <summary>
	/// Gets or sets the operation type (for example "update" or "create").
	/// </summary>
	[AliasAs("op_type")]
	public string? OpType { get; set; }

	/// <summary>
	/// Gets or sets the page number (1-based).
	/// </summary>
	[AliasAs("page")]
	public int Page { get; set; } = 1;

	/// <summary>
	/// Gets or sets the model name (for example "Incident").
	/// </summary>
	[AliasAs("model")]
	public string? Model { get; set; }

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
	/// Gets or sets whether the request is made in portal mode.
	/// </summary>
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}
