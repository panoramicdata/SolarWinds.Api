namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a contract item.
/// </summary>
public sealed class ContractItemCreateRequest
{
	/// <summary>
	/// Gets or sets the contract-item payload to create.
	/// </summary>
	public ContractItemWriteFields Item { get; set; } = new();
}
