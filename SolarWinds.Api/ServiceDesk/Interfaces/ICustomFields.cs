using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Custom Fields API.
/// </summary>
public interface ICustomFields
{
	/// <summary>
	/// Gets custom field definitions for all models.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of custom field definitions across all models.</returns>
	[Get("/custom_fields/ALL_MODELS.json")]
	public Task<List<CustomFieldDefinition>> GetAllModelsAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets the custom fields that can be used as potential dynamic approval sources.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of potential dynamic approval custom fields.</returns>
	[Get("/custom_fields/get_potential_dynamic_approvals.json")]
	public Task<List<PotentialDynamicApproval>> GetPotentialDynamicApprovalsAsync(CancellationToken cancellationToken);
}
