namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a contract.
/// </summary>
public sealed class ContractCreateRequest
{
	public ContractWriteFields Contract { get; set; } = new();
}
