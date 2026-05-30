namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable purchase fields used by create and update operations.
/// </summary>
public sealed class PurchaseWriteFields
{
	/// <summary>
	/// Gets or sets the purchase number.
	/// </summary>
	public string? Number { get; set; }

	/// <summary>
	/// Gets or sets the purchase date string as expected by the API.
	/// </summary>
	public string? Date { get; set; }

	/// <summary>
	/// Gets or sets recurrence metadata for recurring purchases.
	/// </summary>
	public string? Recurrence { get; set; }

	/// <summary>
	/// Gets or sets total cost value.
	/// </summary>
	public string? TotalCost { get; set; }

	/// <summary>
	/// Gets or sets currency code or label.
	/// </summary>
	public string? Currency { get; set; }

	/// <summary>
	/// Gets or sets free-form purchase notes.
	/// </summary>
	public string? Notes { get; set; }

	/// <summary>
	/// Gets or sets vendor payload expected by the API.
	/// </summary>
	public object? Vendor { get; set; }
}
