using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Purchase Orders API.
/// </summary>
public interface IPurchaseOrders
{
	/// <summary>
	/// Gets a list of purchase orders.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of purchase orders.</returns>
	[Get("/api/v2/purchase_orders.json")]
	Task<List<PurchaseOrder>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific purchase order by ID.
	/// </summary>
	/// <param name="id">The ID of the purchase order.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The purchase order.</returns>
	[Get("/api/v2/purchase_orders/{id}.json")]
	Task<PurchaseOrder> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new purchase order.
	/// </summary>
	/// <param name="purchaseOrder">The purchase order to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created purchase order.</returns>
	[Post("/api/v2/purchase_orders.json")]
	Task<PurchaseOrder> CreateAsync([Body] PurchaseOrder purchaseOrder, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing purchase order.
	/// </summary>
	/// <param name="id">The ID of the purchase order to update.</param>
	/// <param name="purchaseOrder">The purchase order data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated purchase order.</returns>
	[Put("/api/v2/purchase_orders/{id}.json")]
	Task<PurchaseOrder> UpdateAsync(int id, [Body] PurchaseOrder purchaseOrder, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a purchase order.
	/// </summary>
	/// <param name="id">The ID of the purchase order to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/purchase_orders/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}