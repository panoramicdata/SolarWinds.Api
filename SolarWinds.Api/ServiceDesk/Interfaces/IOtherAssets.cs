using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Other Assets API.
/// </summary>
public interface IOtherAssets
{
	/// <summary>
	/// Gets a list of other assets.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of other assets.</returns>
	[Get("/api/v2/other_assets.json")]
	public Task<List<Asset>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific other asset by ID.
	/// </summary>
	/// <param name="id">The ID of the other asset.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The other asset.</returns>
	[Get("/api/v2/other_assets/{id}.json")]
	public Task<Asset> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new other asset.
	/// </summary>
	/// <param name="asset">The other asset to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created other asset.</returns>
	[Post("/api/v2/other_assets.json")]
	public Task<Asset> CreateAsync(
		[Body] Asset asset,
		CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing other asset.
	/// </summary>
	/// <param name="id">The ID of the other asset to update.</param>
	/// <param name="asset">The other asset data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated other asset.</returns>
	[Put("/api/v2/other_assets/{id}.json")]
	public Task<Asset> UpdateAsync(
		int id,
		[Body] Asset asset,
		CancellationToken cancellationToken);

	/// <summary>
	/// Deletes an other asset.
	/// </summary>
	/// <param name="id">The ID of the other asset to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/other_assets/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}