namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable contract item fields used by create and update operations.
/// </summary>
public sealed class ContractItemWriteFields
{
	public string? Name { get; set; }
	public string? Description { get; set; }
	public int? Quantity { get; set; }
	public decimal? UnitPrice { get; set; }
}
