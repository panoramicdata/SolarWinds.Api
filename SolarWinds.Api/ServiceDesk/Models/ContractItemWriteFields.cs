namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable contract item fields used by create and update operations.
/// </summary>
public sealed class ContractItemWriteFields
{
	/// <summary>
	/// Gets or sets item name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets item description.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets item quantity.
	/// </summary>
	public int? Quantity { get; set; }

	/// <summary>
	/// Gets or sets item unit price.
	/// </summary>
	public decimal? UnitPrice { get; set; }
}
