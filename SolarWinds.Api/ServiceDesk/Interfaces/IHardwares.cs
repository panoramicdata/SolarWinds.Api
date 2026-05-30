using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Hardware API.
/// </summary>
public interface IHardwares
{
	/// <summary>
	/// Gets a list of hardware assets with query options.
	/// </summary>
	/// <param name="request">The query request.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of hardware assets.</returns>
	[Get("/hardwares.json")]
	public Task<List<Hardware>> GetAsync([Query] GetHardwaresRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific hardware asset by ID with layout option.
	/// </summary>
	/// <param name="id">The ID of the hardware asset.</param>
	/// <param name="layout">The response layout.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The hardware asset.</returns>
	[Get("/hardwares/{id}.json")]
	public Task<Hardware> GetAsync(int id, [AliasAs("layout")] ResponseLayout layout, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new hardware asset.
	/// </summary>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created hardware asset.</returns>
	[Post("/hardwares.json")]
	public Task<Hardware> CreateAsync([Body] HardwareCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing hardware asset.
	/// </summary>
	/// <param name="id">The ID of the hardware asset to update.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated hardware asset.</returns>
	[Put("/hardwares/{id}.json")]
	public Task<Hardware> UpdateAsync(int id, [Body] HardwareUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a hardware asset.
	/// </summary>
	/// <param name="id">The ID of the hardware asset to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/hardwares/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);

	/// <summary>
	/// Gets warranties for a hardware asset.
	/// </summary>
	/// <param name="id">The hardware ID.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The warranties for the hardware asset.</returns>
	[Get("/hardwares/{id}/warranties.json")]
	public Task<List<Warranty>> GetWarrantiesAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a warranty for a hardware asset.
	/// </summary>
	/// <param name="id">The hardware ID.</param>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created warranty.</returns>
	[Post("/hardwares/{id}/warranties.json")]
	public Task<Warranty> CreateWarrantyAsync(int id, [Body] WarrantyCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates a warranty for a hardware asset.
	/// </summary>
	/// <param name="hardwareId">The hardware ID.</param>
	/// <param name="warrantyId">The warranty ID.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated warranty.</returns>
	[Put("/hardwares/{hardwareId}/warranties/{warrantyId}.json")]
	public Task<Warranty> UpdateWarrantyAsync(int hardwareId, int warrantyId, [Body] WarrantyUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a warranty for a hardware asset.
	/// </summary>
	/// <param name="hardwareId">The hardware ID.</param>
	/// <param name="warrantyId">The warranty ID.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/hardwares/{hardwareId}/warranties/{warrantyId}.json")]
	public Task DeleteWarrantyAsync(int hardwareId, int warrantyId, CancellationToken cancellationToken);
}