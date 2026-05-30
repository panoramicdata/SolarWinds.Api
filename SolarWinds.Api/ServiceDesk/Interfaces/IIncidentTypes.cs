using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Incident Types API.
/// </summary>
public interface IIncidentTypes
{
	/// <summary>
	/// Gets a paginated list of incident types, optionally filtered by parent ID and model.
	/// </summary>
	/// <param name="request">The query parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of incident types.</returns>
	[Get("/incident_types/types_list.json")]
	public Task<List<IncidentType>> GetTypesListAsync([Query] GetIncidentTypesRequest request, CancellationToken cancellationToken);
}
