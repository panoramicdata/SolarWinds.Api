namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a purchase order.
/// </summary>
public sealed class PurchaseOrderCreateRequest
{
	public PurchaseOrderWriteFields PurchaseOrder { get; set; } = new();
}
