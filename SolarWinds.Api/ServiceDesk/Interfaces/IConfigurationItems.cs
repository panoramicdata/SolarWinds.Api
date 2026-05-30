using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Configuration Items API.
/// </summary>
public interface IConfigurationItems
{
	/// <summary>
	/// Gets a list of configuration items with query options.
	/// </summary>
	/// <param name="request">The query request.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of configuration items.</returns>
	[Get("/configuration_items.json")]
	public Task<List<ConfigurationItem>> GetAsync([Query] GetConfigurationItemsRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific configuration item by ID with layout option.
	/// </summary>
	/// <param name="id">The ID of the configuration item.</param>
	/// <param name="layout">The response layout.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The configuration item.</returns>
	[Get("/configuration_items/{id}.json")]
	public Task<ConfigurationItem> GetAsync(int id, [AliasAs("layout")] ResponseLayout layout, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new configuration item.
	/// </summary>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created configuration item.</returns>
	[Post("/configuration_items.json")]
	public Task<ConfigurationItem> CreateAsync([Body] ConfigurationItemCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing configuration item.
	/// </summary>
	/// <param name="id">The ID of the configuration item to update.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated configuration item.</returns>
	[Put("/configuration_items/{id}.json")]
	public Task<ConfigurationItem> UpdateAsync(int id, [Body] ConfigurationItemUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a configuration item.
	/// </summary>
	/// <param name="id">The ID of the configuration item to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/configuration_items/{id}.json")]
	public Task DeleteAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Appends dependent assets to a configuration item.
	/// </summary>
	/// <param name="id">The ID of the configuration item.</param>
	/// <param name="request">The append request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Put("/configuration_items/{id}/append_multiple_dependent_assets.json")]
	public Task AppendDependentAssetsAsync(int id, [Body] AppendDependentAssetsRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes an asset link from a configuration item.
	/// </summary>
	/// <param name="request">The delete asset link request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Put("/asset_links/delete_asset_link_by_id.json")]
	public Task DeleteAssetLinkAsync(
		[Body] DeleteAssetLinkRequest request,
		CancellationToken cancellationToken);
}