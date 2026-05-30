namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a contract.
/// </summary>
public sealed class ContractCreateRequest
{
	/// <summary>
	/// Gets or sets the contract payload to create.
	/// </summary>
	public ContractWriteFields Contract { get; set; } = new();
}
