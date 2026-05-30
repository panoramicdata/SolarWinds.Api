namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a contract.
/// </summary>
public sealed class ContractUpdateRequest
{
	public ContractWriteFields Contract { get; set; } = new();
}
