using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Purchases API.
/// </summary>
public interface IPurchases
{
	/// <summary>
	/// Creates a new purchase.
	/// </summary>
	/// <param name="objectType">The source object type (for example, contracts).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created purchase.</returns>
	[Post("/{objectType}/{id}/purchases.json")]
	public Task<Purchase> CreateAsync(ObjectType objectType, int id, [Body] PurchaseCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing purchase.
	/// </summary>
	/// <param name="objectType">The source object type (for example, contracts).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="purchaseId">The purchase ID.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated purchase.</returns>
	[Put("/{objectType}/{id}/purchases/{purchaseId}.json")]
	public Task<Purchase> UpdateAsync(ObjectType objectType, int id, int purchaseId, [Body] PurchaseUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a purchase.
	/// </summary>
	/// <param name="objectType">The source object type (for example, contracts).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="purchaseId">The purchase ID.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/{objectType}/{id}/purchases/{purchaseId}.json")]
	public Task DeleteAsync(
		ObjectType objectType,
		int id,
		int purchaseId,
		CancellationToken cancellationToken);
}