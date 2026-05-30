namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable contract fields used by create and update operations.
/// </summary>
public sealed class ContractWriteFields
{
	public string? Name { get; set; }
	public string? Description { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public int? VendorId { get; set; }
	public decimal? Cost { get; set; }
	public string? Status { get; set; }
}
