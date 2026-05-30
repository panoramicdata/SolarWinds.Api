using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Incidents API.
/// </summary>
public interface IIncidents
{
	/// <summary>
	/// Gets a list of incidents.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of incidents.</returns>
	[Get("/incidents.json")]
	public Task<List<Incident>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a list of incidents using query parameters.
	/// </summary>
	/// <param name="request">The incident query parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of incidents.</returns>
	[Get("/incidents.json")]
	public Task<List<Incident>> GetAllAsync([Query] GetIncidentsRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a page of incidents.
	/// </summary>
	/// <param name="page">The page number (1-based).</param>
	/// <param name="perPage">The number of items per page.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of incidents.</returns>
	[Get("/incidents.json")]
	public Task<List<Incident>> GetPageAsync([AliasAs("page")] int page, [AliasAs("per_page")] int perPage, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific incident by ID.
	/// </summary>
	/// <param name="id">The ID of the incident.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The incident.</returns>
	[Get("/incidents/{id}.json")]
	public Task<Incident> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new incident.
	/// </summary>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created incident.</returns>
	[Post("/incidents.json")]
	public Task<Incident> CreateAsync([Body] IncidentCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing incident.
	/// </summary>
	/// <param name="id">The ID of the incident to update.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated incident.</returns>
	[Put("/incidents/{id}.json")]
	public Task<Incident> UpdateAsync(int id, [Body] IncidentUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes an incident.
	/// </summary>
	/// <param name="id">The ID of the incident to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/incidents/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}