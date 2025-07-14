using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk purchase order.
/// </summary>
public class PurchaseOrder
{
	/// <summary>
	/// Gets or sets the purchase order ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the purchase order name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the purchase order description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the purchase order status.
	/// </summary>
	public string Status { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}