using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for resolving augmented titles for records.
/// </summary>
public sealed class GetAugmentedTitlesRequest
{
	/// <summary>
	/// One or more record ids to resolve.
	/// </summary>
	[AliasAs("ids[]")]
	public int[]? Ids { get; set; }

	/// <summary>
	/// Object model name (for this use case: "incident").
	/// </summary>
	[AliasAs("model")]
	public string? Model { get; set; }

	/// <summary>
	/// Whether to return unmasked titles.
	/// </summary>
	[AliasAs("unmasked")]
	public bool? Unmasked { get; set; }

	/// <summary>
	/// Whether the call is in portal mode.
	/// </summary>
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}
