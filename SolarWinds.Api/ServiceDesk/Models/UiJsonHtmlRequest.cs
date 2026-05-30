using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Generic query parameters observed on UI .jsonhtml list endpoints.
/// </summary>
public sealed class UiJsonHtmlRequest
{
	/// <summary>
	/// Gets or sets report identifier.
	/// </summary>
	[AliasAs("report_id")]
	public int? ReportId { get; set; }

	/// <summary>
	/// Gets or sets request identifier used by the UI.
	/// </summary>
	[AliasAs("requestId")]
	public string? RequestId { get; set; }

	/// <summary>
	/// Gets or sets real-time connection identifier.
	/// </summary>
	[AliasAs("connectionId")]
	public string? ConnectionId { get; set; }

	/// <summary>
	/// Gets or sets sort column.
	/// </summary>
	[AliasAs("sort_by")]
	public string? SortBy { get; set; }

	/// <summary>
	/// Gets or sets sort order.
	/// </summary>
	[AliasAs("sort_order")]
	public string? SortOrder { get; set; }

	/// <summary>
	/// Gets or sets software filter identifier.
	/// </summary>
	[AliasAs("software")]
	public int? Software { get; set; }

	/// <summary>
	/// Gets or sets hidden-printer identifiers.
	/// </summary>
	[AliasAs("hidden_printers[]")]
	public int[]? HiddenPrinters { get; set; }

	/// <summary>
	/// Gets or sets printer type identifiers.
	/// </summary>
	[AliasAs("printer_type[]")]
	public int[]? PrinterType { get; set; }

	/// <summary>
	/// Gets or sets state identifiers that should be excluded.
	/// </summary>
	[AliasAs("state_is_not[]")]
	public int[]? StateIsNot { get; set; }
}
