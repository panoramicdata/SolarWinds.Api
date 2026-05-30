using Refit;
using System.Text.Json;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Generic interface for UI list endpoints ending with .jsonhtml.
/// </summary>
public interface IUiJsonHtmlLists
{
	[Get("/{context}.jsonhtml")]
	public Task<JsonElement> GetAsync(string context, [Query] UiJsonHtmlRequest request, CancellationToken cancellationToken);
}
