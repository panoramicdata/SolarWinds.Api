namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a printer.
/// </summary>
public sealed class PrinterUpdateRequest
{
	public PrinterWriteFields Printer { get; set; } = new();
}
