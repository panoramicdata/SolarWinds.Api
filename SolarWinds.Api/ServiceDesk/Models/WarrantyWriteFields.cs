namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable warranty fields used by create and update operations.
/// </summary>
public sealed class WarrantyWriteFields
{
	/// <summary>
	/// Gets or sets service name.
	/// </summary>
	public string? Service { get; set; }

	/// <summary>
	/// Gets or sets warranty provider name.
	/// </summary>
	public string? Provider { get; set; }

	/// <summary>
	/// Gets or sets warranty start date.
	/// </summary>
	public string? StartDate { get; set; }

	/// <summary>
	/// Gets or sets warranty end date.
	/// </summary>
	public string? EndDate { get; set; }

	/// <summary>
	/// Gets or sets warranty status.
	/// </summary>
	public string? Status { get; set; }

	/// <summary>
	/// Gets or sets whether the warranty is manually entered.
	/// </summary>
	public bool? Manual { get; set; }
}
