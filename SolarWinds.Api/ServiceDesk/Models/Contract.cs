using System;
using System.Collections.Generic;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk contract.
/// </summary>
public class Contract
{
	/// <summary>
	/// Gets or sets the contract ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the contract name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the contract description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the contract start date.
	/// </summary>
	public DateTime StartDate { get; set; }

	/// <summary>
	/// Gets or sets the contract end date.
	/// </summary>
	public DateTime EndDate { get; set; }

	/// <summary>
	/// Gets or sets the vendor ID associated with the contract.
	/// </summary>
	public int VendorId { get; set; }

	/// <summary>
	/// Gets or sets the cost of the contract.
	/// </summary>
	public decimal Cost { get; set; }

	/// <summary>
	/// Gets or sets the contract status.
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

/// <summary>
/// Represents a Service Desk contract item.
/// </summary>
public class ContractItem
{
	/// <summary>
	/// Gets or sets the contract item ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the contract item name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the contract item description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the contract ID this item belongs to.
	/// </summary>
	public int ContractId { get; set; }

	/// <summary>
	/// Gets or sets the quantity of the item.
	/// </summary>
	public int Quantity { get; set; }

	/// <summary>
	/// Gets or sets the unit price of the item.
	/// </summary>
	public decimal UnitPrice { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}