using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for dashboard endpoints.
/// </summary>
public interface IDashboards
{
	/// <summary>
	/// Lists dashboard summaries.
	/// </summary>
	/// <param name="request">Portal-mode options for the dashboard query.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>Dashboard summaries visible to the caller.</returns>
	[Get("/dashboards.json")]
	public Task<List<UiDashboardSummary>> GetAsync([Query] PortalModeRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets dashboard details by identifier.
	/// </summary>
	/// <param name="id">Dashboard identifier.</param>
	/// <param name="request">Portal-mode options for the dashboard query.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>Dashboard detail payload.</returns>
	[Get("/dashboards/{id}.json")]
	public Task<UiDashboardDetail> GetAsync(int id, [Query] PortalModeRequest request, CancellationToken cancellationToken);
}
