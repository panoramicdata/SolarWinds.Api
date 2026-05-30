using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for Service Desk Change Request creation.
/// </summary>
public interface IChangeRequests
{
	[Post("/change_catalogs/{id}/change_requests.json")]
	public Task<Change> CreateAsync(int id, [Body] ChangeRequestCreateRequest request, CancellationToken cancellationToken);
}
