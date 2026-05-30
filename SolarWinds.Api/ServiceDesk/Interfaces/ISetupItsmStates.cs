using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for setup ITSM state initialization endpoints.
/// </summary>
public interface ISetupItsmStates
{
	/// <summary>
	/// Initializes tab visibility/state metadata for ITSM custom states setup.
	/// </summary>
	/// <param name="request">The setup tab initialization query parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The tabs initialization payload.</returns>
	[Get("/setup/itsm_states/init_tabs_data.json")]
	public Task<ItsmStatesInitTabsDataResponse> InitTabsDataAsync([Query] GetItsmStatesInitTabsDataRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Initializes states data for an ITSM type.
	/// </summary>
	/// <param name="request">The states initialization query parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The states initialization payload.</returns>
	[Get("/setup/itsm_states/init_states_data.json")]
	public Task<ItsmStatesInitStatesDataResponse> InitStatesDataAsync([Query] GetItsmStatesInitStatesDataRequest request, CancellationToken cancellationToken);
}
