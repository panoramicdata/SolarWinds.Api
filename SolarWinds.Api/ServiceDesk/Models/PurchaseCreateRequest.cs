namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a purchase record.
/// </summary>
public sealed class PurchaseCreateRequest
{
	/// <summary>
	/// Gets or sets the purchase fields to create.
	/// </summary>
	public PurchaseWriteFields Purchase { get; set; } = new();
}
