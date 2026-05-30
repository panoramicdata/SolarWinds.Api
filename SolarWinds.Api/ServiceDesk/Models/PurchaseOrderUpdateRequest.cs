namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a purchase order.
/// </summary>
public sealed class PurchaseOrderUpdateRequest
{
	public PurchaseOrderWriteFields PurchaseOrder { get; set; } = new();
}
