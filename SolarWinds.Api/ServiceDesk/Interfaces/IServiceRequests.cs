using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Service Requests API.
/// Note: The API documentation primarily supports creation of service requests.
/// Retrieval and updates might be handled via Incident API endpoints.
/// </summary>
public interface IServiceRequests
{
	/// <summary>
	/// Creates a new service request from a catalog item.
	/// </summary>
	/// <param name="id">The catalog item ID.</param>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created incident.</returns>
	[Post("/catalog_items/{id}/service_requests.json")]
	public Task<Incident> CreateAsync(
		int id,
		[Body] ServiceRequestCreateRequest request,
		CancellationToken cancellationToken);
}