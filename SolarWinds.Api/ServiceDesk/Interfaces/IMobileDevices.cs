using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Mobile Devices API.
/// </summary>
public interface IMobileDevices
{
	/// <summary>
	/// Gets a list of mobile devices.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of mobile devices.</returns>
	[Get("/mobiles.json")]
	public Task<List<MobileDevice>> GetAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific mobile device by ID.
	/// </summary>
	/// <param name="id">The ID of the mobile device.</param>
	/// <param name="layout">The response layout.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The mobile device.</returns>
	[Get("/mobiles/{id}.json")]
	public Task<MobileDevice> GetAsync(int id, [AliasAs("layout")] ResponseLayout layout, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new mobile device.
	/// </summary>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created mobile device.</returns>
	[Post("/mobiles.json")]
	public Task<MobileDevice> CreateAsync([Body] MobileDeviceCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing mobile device.
	/// </summary>
	/// <param name="id">The ID of the mobile device to update.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated mobile device.</returns>
	[Put("/mobiles/{id}.json")]
	public Task<MobileDevice> UpdateAsync(int id, [Body] MobileDeviceUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a mobile device.
	/// </summary>
	/// <param name="id">The ID of the mobile device to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/mobiles/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}