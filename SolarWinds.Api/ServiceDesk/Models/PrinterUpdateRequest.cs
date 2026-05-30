namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a printer.
/// </summary>
public sealed class PrinterUpdateRequest
{
	/// <summary>
	/// Gets or sets the printer fields to update.
	/// </summary>
	public PrinterWriteFields Printer { get; set; } = new();
}
