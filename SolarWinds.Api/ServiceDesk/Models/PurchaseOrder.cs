using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk purchase order.
/// </summary>
public class PurchaseOrder : DescribedEntity
{
	/// <summary>
	/// Gets or sets the purchase order status.
	/// </summary>
	public string Status { get; set; } = string.Empty;
}