namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a purchase order.
/// </summary>
public sealed class PurchaseOrderCreateRequest
{
	/// <summary>
	/// Gets or sets the purchase-order payload to create.
	/// </summary>
	public PurchaseOrderWriteFields PurchaseOrder { get; set; } = new();
}
