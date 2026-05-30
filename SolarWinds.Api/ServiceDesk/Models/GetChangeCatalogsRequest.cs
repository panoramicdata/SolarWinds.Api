using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for listing change catalogs with UI tree options.
/// </summary>
public sealed class GetChangeCatalogsRequest
{
	[AliasAs("type")]
	public string? Type { get; set; }

	[AliasAs("newButton")]
	public bool? NewButton { get; set; }

	[AliasAs("state")]
	public int? State { get; set; }

	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}
