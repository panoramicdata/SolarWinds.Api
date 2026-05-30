using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Contracts API.
/// </summary>
public interface IContracts
{
	/// <summary>
	/// Gets a list of contracts.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of contracts.</returns>
	[Get("/contracts.json")]
	public Task<List<Contract>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a list of contracts with query options.
	/// </summary>
	/// <param name="request">The query request.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of contracts.</returns>
	[Get("/contracts.json")]
	public Task<List<Contract>> GetAllAsync([Query] GetContractsRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific contract by ID.
	/// </summary>
	/// <param name="id">The ID of the contract.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The contract.</returns>
	[Get("/contracts/{id}.json")]
	public Task<Contract> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific contract by ID with layout option.
	/// </summary>
	/// <param name="id">The ID of the contract.</param>
	/// <param name="layout">The response layout.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The contract.</returns>
	[Get("/contracts/{id}.json")]
	public Task<Contract> GetAsync(int id, [AliasAs("layout")] ResponseLayout? layout, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new contract.
	/// </summary>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created contract.</returns>
	[Post("/contracts.json")]
	public Task<Contract> CreateAsync([Body] ContractCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing contract.
	/// </summary>
	/// <param name="id">The ID of the contract to update.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated contract.</returns>
	[Put("/contracts/{id}.json")]
	public Task<Contract> UpdateAsync(int id, [Body] ContractUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a contract.
	/// </summary>
	/// <param name="id">The ID of the contract to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/contracts/{id}.json")]
	public Task DeleteAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new contract item.
	/// </summary>
	/// <param name="id">The contract ID.</param>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created contract item.</returns>
	[Post("/contracts/{id}/items.json")]
	public Task<ContractItem> CreateItemAsync(int id, [Body] ContractItemCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing contract item.
	/// </summary>
	/// <param name="contractId">The contract ID.</param>
	/// <param name="itemId">The item ID.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated contract item.</returns>
	[Put("/contracts/{contractId}/items/{itemId}.json")]
	public Task<ContractItem> UpdateItemAsync(int contractId, int itemId, [Body] ContractItemUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a contract item.
	/// </summary>
	/// <param name="contractId">The contract ID.</param>
	/// <param name="itemId">The item ID.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/contracts/{contractId}/items/{itemId}.json")]
	public Task DeleteItemAsync(
		int contractId,
		int itemId,
		CancellationToken cancellationToken);
}