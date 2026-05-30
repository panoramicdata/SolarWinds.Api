using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Other Assets API.
/// </summary>
public interface IOtherAssets
{
	/// <summary>
	/// Gets a list of other assets with query options.
	/// </summary>
	/// <param name="request">The query request.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of other assets.</returns>
	[Get("/other_assets.json")]
	public Task<List<Asset>> GetAsync([Query] GetOtherAssetsRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific other asset by ID with layout option.
	/// </summary>
	/// <param name="id">The ID of the other asset.</param>
	/// <param name="layout">The response layout.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The other asset.</returns>
	[Get("/other_assets/{id}.json")]
	public Task<Asset> GetAsync(int id, [AliasAs("layout")] ResponseLayout layout, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new other asset.
	/// </summary>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created other asset.</returns>
	[Post("/other_assets.json")]
	public Task<Asset> CreateAsync(
		[Body] OtherAssetCreateRequest request,
		CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing other asset.
	/// </summary>
	/// <param name="id">The ID of the other asset to update.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated other asset.</returns>
	[Put("/other_assets/{id}.json")]
	public Task<Asset> UpdateAsync(
		int id,
		[Body] OtherAssetUpdateRequest request,
		CancellationToken cancellationToken);

	/// <summary>
	/// Deletes an other asset.
	/// </summary>
	/// <param name="id">The ID of the other asset to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/other_assets/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}