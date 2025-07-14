using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Configuration Items API.
/// </summary>
public interface IConfigurationItems
{
	/// <summary>
	/// Gets a list of configuration items.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of configuration items.</returns>
	[Get("/api/v2/configuration_items.json")]
	Task<List<ConfigurationItem>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific configuration item by ID.
	/// </summary>
	/// <param name="id">The ID of the configuration item.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The configuration item.</returns>
	[Get("/api/v2/configuration_items/{id}.json")]
	Task<ConfigurationItem> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new configuration item.
	/// </summary>
	/// <param name="configurationItem">The configuration item to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created configuration item.</returns>
	[Post("/api/v2/configuration_items.json")]
	Task<ConfigurationItem> CreateAsync([Body] ConfigurationItem configurationItem, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing configuration item.
	/// </summary>
	/// <param name="id">The ID of the configuration item to update.</param>
	/// <param name="configurationItem">The configuration item data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated configuration item.</returns>
	[Put("/api/v2/configuration_items/{id}.json")]
	Task<ConfigurationItem> UpdateAsync(int id, [Body] ConfigurationItem configurationItem, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a configuration item.
	/// </summary>
	/// <param name="id">The ID of the configuration item to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/configuration_items/{id}.json")]
	Task DeleteAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Appends dependent assets to a configuration item.
	/// </summary>
	/// <param name="id">The ID of the configuration item.</param>
	/// <param name="assetIds">The IDs of the assets to append.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Put("/api/v2/configuration_items/{id}/append_dependent_assets.json")]
	Task AppendDependentAssetsAsync(int id, [Body] IEnumerable<int> assetIds, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes an asset link from a configuration item.
	/// </summary>
	/// <param name="id">The ID of the configuration item.</param>
	/// <param name="assetId">The ID of the asset link to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Put("/api/v2/configuration_items/{id}/delete_asset_link.json")]
	public Task DeleteAssetLinkAsync(
		int id,
		[Body] int assetId,
		CancellationToken cancellationToken);
}