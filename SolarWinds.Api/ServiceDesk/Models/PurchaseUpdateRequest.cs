namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a purchase record.
/// </summary>
public sealed class PurchaseUpdateRequest
{
	/// <summary>
	/// Gets or sets the purchase fields to update.
	/// </summary>
	public PurchaseWriteFields Purchase { get; set; } = new();
}
