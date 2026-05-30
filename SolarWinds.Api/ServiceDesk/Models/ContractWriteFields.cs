namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable contract fields used by create and update operations.
/// </summary>
public sealed class ContractWriteFields
{
	/// <summary>
	/// Gets or sets contract name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets contract description.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets contract start date.
	/// </summary>
	public DateTime? StartDate { get; set; }

	/// <summary>
	/// Gets or sets contract end date.
	/// </summary>
	public DateTime? EndDate { get; set; }

	/// <summary>
	/// Gets or sets vendor identifier.
	/// </summary>
	public int? VendorId { get; set; }

	/// <summary>
	/// Gets or sets total contract cost.
	/// </summary>
	public decimal? Cost { get; set; }

	/// <summary>
	/// Gets or sets contract status.
	/// </summary>
	public string? Status { get; set; }
}
