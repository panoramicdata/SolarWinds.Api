using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Custom Forms API.
/// </summary>
public interface ICustomForms
{
	/// <summary>
	/// Gets a custom form definition by ID.
	/// </summary>
	/// <param name="id">The custom form ID.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The custom form definition.</returns>
	[Get("/setup/custom_forms/{id}.json")]
	public Task<CustomForm> GetAsync(int id, CancellationToken cancellationToken);
}
