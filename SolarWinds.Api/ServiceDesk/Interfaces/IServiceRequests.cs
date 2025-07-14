using Refit;
using System.Threading;
using System.Threading.Tasks;
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
	/// Creates a new service request.
	/// </summary>
	/// <param name="serviceRequest">The service request to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created service request.</returns>
	[Post("/api/v2/service_requests.json")]
	public Task<ServiceRequest> CreateAsync(
		[Body] ServiceRequest serviceRequest,
		CancellationToken cancellationToken);
}