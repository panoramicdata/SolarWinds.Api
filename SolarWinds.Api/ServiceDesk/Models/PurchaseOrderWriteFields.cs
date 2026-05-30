namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable purchase order fields used by create and update operations.
/// </summary>
public sealed class PurchaseOrderWriteFields
{
	public string? Name { get; set; }
	public string? Description { get; set; }
	public string? Status { get; set; }
}
