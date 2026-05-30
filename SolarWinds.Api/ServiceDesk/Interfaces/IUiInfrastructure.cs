using Refit;
using System.Text.Json;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for shared UI infrastructure endpoints.
/// </summary>
public interface IUiInfrastructure
{
	/// <summary>
	/// Gets shared custom context data used by the portal shell.
	/// </summary>
	/// <param name="request">Custom-context query parameters.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>Raw custom context JSON payload.</returns>
	[Get("/custom.json")]
	public Task<JsonElement> GetCustomContextAsync([Query] UiCustomContextRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets URL filter definitions for the current portal context.
	/// </summary>
	/// <param name="request">URL filter query parameters.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>Raw URL-filter JSON payload.</returns>
	[Get("/filters/url_filters.json")]
	public Task<JsonElement> GetUrlFiltersAsync([Query] UiUrlFiltersRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Requests a WebSocket JWT token for real-time portal channels.
	/// </summary>
	/// <param name="request">Portal-mode query parameters.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>Raw JWT response payload.</returns>
	[Get("/websockets/jwt.json")]
	public Task<JsonElement> GetWebsocketJwtAsync([Query] PortalModeRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets the RUM bootstrap script metadata used by the portal UI.
	/// </summary>
	/// <param name="request">Portal-mode query parameters.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>Raw RUM script payload.</returns>
	[Get("/rum_script.json")]
	public Task<JsonElement> GetRumScriptAsync([Query] PortalModeRequest request, CancellationToken cancellationToken);
}
