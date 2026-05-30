namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class PurchaseUpdateRequest
{
	public PurchaseWriteFields Purchase { get; set; } = new();
}
