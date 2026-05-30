namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a contract item.
/// </summary>
public sealed class ContractItemCreateRequest
{
	public ContractItemWriteFields Item { get; set; } = new();
}
