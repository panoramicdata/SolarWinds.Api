using Refit;
using System.Text.Json;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for dashboard widget endpoints.
/// </summary>
public interface IWidgets
{
	[Get("/widgets/{id}.json")]
	public Task<UiWidgetDefinition> GetAsync(int id, [Query] UiWidgetRequest request, CancellationToken cancellationToken);

	[Get("/widgets/data.json")]
	public Task<JsonElement> GetDataAsync([Query] UiWidgetDataRequest request, CancellationToken cancellationToken);
}
