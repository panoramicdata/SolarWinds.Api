using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for listing change catalogs with UI tree options.
/// </summary>
public sealed class GetChangeCatalogsRequest
{
	/// <summary>
	/// Gets or sets the list type selector used by the portal view.
	/// </summary>
	[AliasAs("type")]
	public string? Type { get; set; }

	/// <summary>
	/// Gets or sets whether the UI should include new-item button metadata.
	/// </summary>
	[AliasAs("newButton")]
	public bool? NewButton { get; set; }

	/// <summary>
	/// Gets or sets optional state filter identifier.
	/// </summary>
	[AliasAs("state")]
	public int? State { get; set; }

	/// <summary>
	/// Gets or sets whether the query should be evaluated in portal mode.
	/// </summary>
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}
