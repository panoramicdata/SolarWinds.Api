using Refit;
using SolarWinds.Api.ServiceDesk.Models;
using System.Text.Json;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Response Templates API.
/// </summary>
public interface IResponseTemplates
{
	/// <summary>
	/// Gets the total count of all response templates.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The total response template count.</returns>
	[Get("/response_templates/total_count.json")]
	public Task<CountResult> GetTotalCountAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets response templates setup list payload.
	/// </summary>
	[Get("/setup/response_templates.json")]
	public Task<JsonElement> GetSetupListAsync([Query] PortalModeRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets the total count of personal response templates for the current user.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The personal response template count.</returns>
	[Get("/response_templates/total_count_personal.json")]
	public Task<CountResult> GetTotalCountPersonalAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets the total count of global response templates.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The global response template count.</returns>
	[Get("/response_templates/total_count_global.json")]
	public Task<CountResult> GetTotalCountGlobalAsync(CancellationToken cancellationToken);
}
