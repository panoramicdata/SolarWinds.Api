using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Incidents API.
/// </summary>
public interface IIncidents
{
	/// <summary>
	/// Gets a list of incidents using query parameters.
	/// </summary>
	/// <param name="request">The incident query parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of incidents.</returns>
	[Get("/incidents.json")]
	public Task<List<Incident>> GetAsync([Query(CollectionFormat.Multi)] GetIncidentsRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific incident by ID.
	/// </summary>
	/// <param name="id">The ID of the incident.</param>
	/// <param name="layout">The response layout.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The incident.</returns>
	[Get("/incidents/{id}.json")]
	public Task<Incident> GetAsync(int id, [AliasAs("layout")] ResponseLayout layout, CancellationToken cancellationToken);

	/// <summary>
	/// Gets incident general information, including available incident states.
	/// </summary>
	/// <param name="incidentId">The ID of the incident.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The incident general information payload.</returns>
	[Get("/entity_general_info/{incidentId}.json?object_type=incident&action_page_type=show&is_portal_mode=false")]
	public Task<IncidentEntityGeneralInfo> GetEntityGeneralInfoAsync(int incidentId, CancellationToken cancellationToken);

	/// <summary>
	/// Resolves augmented titles for one or more incidents.
	/// </summary>
	/// <param name="request">Augmented title query parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of resolved augmented titles.</returns>
	[Get("/augmented_titles.json")]
	public Task<List<AugmentedTitleResponse>> GetAugmentedTitlesAsync([Query] GetAugmentedTitlesRequest request, CancellationToken cancellationToken);

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

	/// <summary>
	/// Gets the service monitor (SLA) statistics for a specific incident.
	/// </summary>
	/// <param name="id">The ID of the incident.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The service monitor statistic.</returns>
	[Get("/incidents/{id}/service_monitor_statistic.json")]
	public Task<ServiceMonitorStatistic> GetServiceMonitorStatisticAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Gets the workflow associated with a specific incident.
	/// </summary>
	/// <param name="id">The ID of the incident.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The incident workflow.</returns>
	[Get("/incidents/{id}/get_workflow.json")]
	public Task<IncidentWorkflow> GetWorkflowAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Gets the response template variables available for a specific incident.
	/// </summary>
	/// <param name="id">The ID of the incident.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of response template variables.</returns>
	[Get("/incidents/{id}/response_template_variables.json")]
	public Task<List<ResponseTemplateVariable>> GetResponseTemplateVariablesAsync(int id, CancellationToken cancellationToken);
}