using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk purchase.
/// </summary>
public class Purchase : DescribedEntity
{
	/// <summary>
	/// Gets or sets the purchase amount.
	/// </summary>
	public decimal Amount { get; set; }

	/// <summary>
	/// Gets or sets the vendor ID associated with the purchase.
	/// </summary>
	public int VendorId { get; set; }
}