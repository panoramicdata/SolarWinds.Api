using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
	[Get("/api/v2/mobiles.json")]
	Task<List<MobileDevice>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific mobile device by ID.
	/// </summary>
	/// <param name="id">The ID of the mobile device.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The mobile device.</returns>
	[Get("/api/v2/mobiles/{id}.json")]
	Task<MobileDevice> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new mobile device.
	/// </summary>
	/// <param name="mobileDevice">The mobile device to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created mobile device.</returns>
	[Post("/api/v2/mobiles.json")]
	Task<MobileDevice> CreateAsync([Body] MobileDevice mobileDevice, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing mobile device.
	/// </summary>
	/// <param name="id">The ID of the mobile device to update.</param>
	/// <param name="mobileDevice">The mobile device data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated mobile device.</returns>
	[Put("/api/v2/mobiles/{id}.json")]
	Task<MobileDevice> UpdateAsync(int id, [Body] MobileDevice mobileDevice, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a mobile device.
	/// </summary>
	/// <param name="id">The ID of the mobile device to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/mobiles/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}