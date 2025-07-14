using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
	[Get("/api/v2/contracts.json")]
	Task<List<Contract>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific contract by ID.
	/// </summary>
	/// <param name="id">The ID of the contract.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The contract.</returns>
	[Get("/api/v2/contracts/{id}.json")]
	Task<Contract> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new contract.
	/// </summary>
	/// <param name="contract">The contract to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created contract.</returns>
	[Post("/api/v2/contracts.json")]
	Task<Contract> CreateAsync([Body] Contract contract, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing contract.
	/// </summary>
	/// <param name="id">The ID of the contract to update.</param>
	/// <param name="contract">The contract data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated contract.</returns>
	[Put("/api/v2/contracts/{id}.json")]
	Task<Contract> UpdateAsync(int id, [Body] Contract contract, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a contract.
	/// </summary>
	/// <param name="id">The ID of the contract to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/contracts/{id}.json")]
	Task DeleteAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new contract item.
	/// </summary>
	/// <param name="contractItem">The contract item to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created contract item.</returns>
	[Post("/api/v2/contracts/items.json")]
	Task<ContractItem> CreateItemAsync([Body] ContractItem contractItem, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing contract item.
	/// </summary>
	/// <param name="id">The ID of the contract item to update.</param>
	/// <param name="contractItem">The contract item data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated contract item.</returns>
	[Put("/api/v2/contracts/items/{id}.json")]
	Task<ContractItem> UpdateItemAsync(int id, [Body] ContractItem contractItem, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a contract item.
	/// </summary>
	/// <param name="id">The ID of the contract item to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/contracts/items/{id}.json")]
	public Task DeleteItemAsync(
		int id,
		CancellationToken cancellationToken);
}