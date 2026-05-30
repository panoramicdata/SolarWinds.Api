namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a contract item.
/// </summary>
public sealed class ContractItemUpdateRequest
{
	/// <summary>
	/// Gets or sets the contract-item fields to update.
	/// </summary>
	public ContractItemWriteFields Item { get; set; } = new();
}
