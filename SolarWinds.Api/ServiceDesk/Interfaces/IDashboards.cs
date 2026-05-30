using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for dashboard endpoints.
/// </summary>
public interface IDashboards
{
	[Get("/dashboards.json")]
	public Task<List<UiDashboardSummary>> GetAsync([Query] PortalModeRequest request, CancellationToken cancellationToken);

	[Get("/dashboards/{id}.json")]
	public Task<UiDashboardDetail> GetAsync(int id, [Query] PortalModeRequest request, CancellationToken cancellationToken);
}
