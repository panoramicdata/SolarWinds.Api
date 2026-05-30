using Refit;
using System.Text.Json;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for shared UI infrastructure endpoints.
/// </summary>
public interface IUiInfrastructure
{
	[Get("/custom.json")]
	public Task<JsonElement> GetCustomContextAsync([Query] UiCustomContextRequest request, CancellationToken cancellationToken);

	[Get("/filters/url_filters.json")]
	public Task<JsonElement> GetUrlFiltersAsync([Query] UiUrlFiltersRequest request, CancellationToken cancellationToken);

	[Get("/websockets/jwt.json")]
	public Task<JsonElement> GetWebsocketJwtAsync([Query] PortalModeRequest request, CancellationToken cancellationToken);

	[Get("/rum_script.json")]
	public Task<JsonElement> GetRumScriptAsync([Query] PortalModeRequest request, CancellationToken cancellationToken);
}
