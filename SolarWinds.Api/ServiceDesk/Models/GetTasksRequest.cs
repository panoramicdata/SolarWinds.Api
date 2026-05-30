using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Query parameters for listing tasks.
/// </summary>
public sealed class GetTasksRequest
{
	/// <summary>
	/// Optional report identifier used by saved/portal report-backed task searches.
	/// </summary>
	[AliasAs("report_id")]
	public int? ReportId { get; set; }

	/// <summary>
	/// Optional portal flag indicating filters are applied.
	/// </summary>
	[AliasAs("applied")]
	public bool? Applied { get; set; }

	/// <summary>
	/// Optional assignee filters using the portal assigned_to[] parameter shape.
	/// </summary>
	[AliasAs("assigned_to[]")]
	public int[]? AssignedTo { get; set; }

	/// <summary>
	/// Optional comma-delimited portal column list.
	/// </summary>
	[AliasAs("columns")]
	public string? Columns { get; set; }
}
