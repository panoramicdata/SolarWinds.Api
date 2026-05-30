using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for Service Desk Change Request creation.
/// </summary>
public interface IChangeRequests
{
	/// <summary>
	/// Creates a change request from a change catalog.
	/// </summary>
	/// <param name="id">Parent change catalog identifier.</param>
	/// <param name="request">Change request payload.</param>
	/// <param name="cancellationToken">Token used to cancel the request.</param>
	/// <returns>The created change record.</returns>
	[Post("/change_catalogs/{id}/change_requests.json")]
	public Task<Change> CreateAsync(int id, [Body] ChangeRequestCreateRequest request, CancellationToken cancellationToken);
}
