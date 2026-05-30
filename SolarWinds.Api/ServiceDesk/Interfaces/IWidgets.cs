using Refit;
using System.Text.Json;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for dashboard widget endpoints.
/// </summary>
public interface IWidgets
{
	/// <summary>
	/// Gets a widget definition by identifier.
	/// </summary>
	/// <param name="id">Widget identifier.</param>
	/// <param name="request">Widget query parameters.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>Widget definition payload.</returns>
	[Get("/widgets/{id}.json")]
	public Task<UiWidgetDefinition> GetAsync(int id, [Query] UiWidgetRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets runtime data for one or more dashboard widgets.
	/// </summary>
	/// <param name="request">Widget data query parameters.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>Raw widget data JSON payload.</returns>
	[Get("/widgets/data.json")]
	public Task<JsonElement> GetDataAsync([Query] UiWidgetDataRequest request, CancellationToken cancellationToken);
}
