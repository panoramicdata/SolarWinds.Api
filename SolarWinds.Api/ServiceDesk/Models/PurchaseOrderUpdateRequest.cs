namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a purchase order.
/// </summary>
public sealed class PurchaseOrderUpdateRequest
{
	/// <summary>
	/// Gets or sets the purchase-order fields to update.
	/// </summary>
	public PurchaseOrderWriteFields PurchaseOrder { get; set; } = new();
}
