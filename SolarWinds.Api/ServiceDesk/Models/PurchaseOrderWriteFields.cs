namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable purchase order fields used by create and update operations.
/// </summary>
public sealed class PurchaseOrderWriteFields
{
	/// <summary>
	/// Gets or sets the purchase-order name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets purchase-order description.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets purchase-order status value.
	/// </summary>
	public string? Status { get; set; }
}
