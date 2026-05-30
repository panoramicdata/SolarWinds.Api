namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class PurchaseWriteFields
{
	public string? Number { get; set; }
	public string? Date { get; set; }
	public string? Recurrence { get; set; }
	public string? TotalCost { get; set; }
	public string? Currency { get; set; }
	public string? Notes { get; set; }
	public object? Vendor { get; set; }
}
