namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a contract item.
/// </summary>
public sealed class ContractItemUpdateRequest
{
	public ContractItemWriteFields Item { get; set; } = new();
}
