namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class PurchaseCreateRequest
{
	public PurchaseWriteFields Purchase { get; set; } = new();
}
