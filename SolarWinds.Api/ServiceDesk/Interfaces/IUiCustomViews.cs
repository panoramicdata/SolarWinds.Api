using Refit;
using System.Text.Json;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for UI custom-views endpoints used by list pages.
/// </summary>
public interface IUiCustomViews
{
	/// <summary>
	/// Retrieves custom-view data for a specific UI context.
	/// </summary>
	/// <param name="context">UI context key (for example, incidents or tasks).</param>
	/// <param name="request">Custom-view query parameters.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>Raw custom-view JSON payload.</returns>
	[Get("/custom_views/{context}.json")]
	public Task<JsonElement> GetViewAsync(string context, [Query] UiCustomViewRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Retrieves custom-view metadata for a specific UI context.
	/// </summary>
	/// <param name="context">UI context key.</param>
	/// <param name="request">Metadata query parameters.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>Raw custom-view metadata JSON payload.</returns>
	[Get("/custom_views/{context}/metadata.json")]
	public Task<JsonElement> GetMetadataAsync(string context, [Query] UiCustomViewMetadataRequest request, CancellationToken cancellationToken);
}
