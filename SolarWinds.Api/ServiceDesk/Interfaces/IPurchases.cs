using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Purchases API.
/// </summary>
public interface IPurchases
{
	/// <summary>
	/// Gets a list of purchases.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of purchases.</returns>
	[Get("/api/v2/purchases.json")]
	Task<List<Purchase>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific purchase by ID.
	/// </summary>
	/// <param name="id">The ID of the purchase.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The purchase.</returns>
	[Get("/api/v2/purchases/{id}.json")]
	Task<Purchase> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new purchase.
	/// </summary>
	/// <param name="purchase">The purchase to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created purchase.</returns>
	[Post("/api/v2/purchases.json")]
	Task<Purchase> CreateAsync([Body] Purchase purchase, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing purchase.
	/// </summary>
	/// <param name="id">The ID of the purchase to update.</param>
	/// <param name="purchase">The purchase data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated purchase.</returns>
	[Put("/api/v2/purchases/{id}.json")]
	Task<Purchase> UpdateAsync(int id, [Body] Purchase purchase, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a purchase.
	/// </summary>
	/// <param name="id">The ID of the purchase to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/purchases/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}