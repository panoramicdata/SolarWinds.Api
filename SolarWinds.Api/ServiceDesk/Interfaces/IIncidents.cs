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
	/// <param name="incident">The incident to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created incident.</returns>
	[Post("/incidents.json")]
	public Task<Incident> CreateAsync([Body] Incident incident, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing incident.
	/// </summary>
	/// <param name="id">The ID of the incident to update.</param>
	/// <param name="incident">The incident data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated incident.</returns>
	[Put("/incidents/{id}.json")]
	public Task<Incident> UpdateAsync(int id, [Body] Incident incident, CancellationToken cancellationToken);

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