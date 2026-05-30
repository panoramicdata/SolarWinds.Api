using Refit;
using System.Text.Json;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Generic interface for UI list endpoints ending with .jsonhtml.
/// </summary>
public interface IUiJsonHtmlLists
{
	/// <summary>
	/// Gets list data for a dynamic <c>.jsonhtml</c> UI context endpoint.
	/// </summary>
	/// <param name="context">Context segment that selects the list endpoint.</param>
	/// <param name="request">List query parameters.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>Raw list JSON payload.</returns>
	[Get("/{context}.jsonhtml")]
	public Task<JsonElement> GetAsync(string context, [Query] UiJsonHtmlRequest request, CancellationToken cancellationToken);
}
