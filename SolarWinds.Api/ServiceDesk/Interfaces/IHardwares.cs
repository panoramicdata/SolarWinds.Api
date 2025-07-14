using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Hardware API.
/// </summary>
public interface IHardwares
{
	/// <summary>
	/// Gets a list of hardware assets.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of hardware assets.</returns>
	[Get("/api/v2/hardwares.json")]
	Task<List<Hardware>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific hardware asset by ID.
	/// </summary>
	/// <param name="id">The ID of the hardware asset.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The hardware asset.</returns>
	[Get("/api/v2/hardwares/{id}.json")]
	Task<Hardware> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new hardware asset.
	/// </summary>
	/// <param name="hardware">The hardware asset to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created hardware asset.</returns>
	[Post("/api/v2/hardwares.json")]
	Task<Hardware> CreateAsync([Body] Hardware hardware, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing hardware asset.
	/// </summary>
	/// <param name="id">The ID of the hardware asset to update.</param>
	/// <param name="hardware">The hardware asset data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated hardware asset.</returns>
	[Put("/api/v2/hardwares/{id}.json")]
	Task<Hardware> UpdateAsync(int id, [Body] Hardware hardware, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a hardware asset.
	/// </summary>
	/// <param name="id">The ID of the hardware asset to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/hardwares/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}