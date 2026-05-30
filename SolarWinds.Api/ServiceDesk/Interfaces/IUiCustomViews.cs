using Refit;
using System.Text.Json;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for UI custom-views endpoints used by list pages.
/// </summary>
public interface IUiCustomViews
{
	[Get("/custom_views/{context}.json")]
	public Task<JsonElement> GetViewAsync(string context, [Query] UiCustomViewRequest request, CancellationToken cancellationToken);

	[Get("/custom_views/{context}/metadata.json")]
	public Task<JsonElement> GetMetadataAsync(string context, [Query] UiCustomViewMetadataRequest request, CancellationToken cancellationToken);
}
