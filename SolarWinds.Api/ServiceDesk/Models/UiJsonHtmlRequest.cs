using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Generic query parameters observed on UI .jsonhtml list endpoints.
/// </summary>
public sealed class UiJsonHtmlRequest
{
	[AliasAs("report_id")]
	public int? ReportId { get; set; }

	[AliasAs("requestId")]
	public string? RequestId { get; set; }

	[AliasAs("connectionId")]
	public string? ConnectionId { get; set; }

	[AliasAs("sort_by")]
	public string? SortBy { get; set; }

	[AliasAs("sort_order")]
	public string? SortOrder { get; set; }

	[AliasAs("software")]
	public int? Software { get; set; }

	[AliasAs("hidden_printers[]")]
	public int[]? HiddenPrinters { get; set; }

	[AliasAs("printer_type[]")]
	public int[]? PrinterType { get; set; }

	[AliasAs("state_is_not[]")]
	public int[]? StateIsNot { get; set; }
}
