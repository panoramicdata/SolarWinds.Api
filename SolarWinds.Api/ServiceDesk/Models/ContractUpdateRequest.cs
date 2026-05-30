namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a contract.
/// </summary>
public sealed class ContractUpdateRequest
{
	/// <summary>
	/// Gets or sets the contract fields to update.
	/// </summary>
	public ContractWriteFields Contract { get; set; } = new();
}
