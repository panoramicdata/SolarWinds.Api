using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk purchase.
/// </summary>
public class Purchase
{
	/// <summary>
	/// Gets or sets the purchase ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the purchase name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the purchase description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the purchase amount.
	/// </summary>
	public decimal Amount { get; set; }

	/// <summary>
	/// Gets or sets the vendor ID associated with the purchase.
	/// </summary>
	public int VendorId { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}