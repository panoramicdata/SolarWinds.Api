namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable incident fields used by create and update operations.
/// </summary>
public sealed class IncidentWriteFields
{
	public string? Name { get; set; }
	public int? SiteId { get; set; }
	public int? DepartmentId { get; set; }
	public string? Description { get; set; }
	public string? DescriptionNoHtml { get; set; }
	public int? StateId { get; set; }
	public string? State { get; set; }
	public object? Assignee { get; set; }
	public object? GroupAssignee { get; set; }
	public object? Requester { get; set; }
	public string? Priority { get; set; }
	public object? Category { get; set; }
	public object? Subcategory { get; set; }
	public DateTime? DueAt { get; set; }
	public string? AddToTagList { get; set; }
	public string? RemoveFromTagList { get; set; }
	public string? TagList { get; set; }
	public object[]? CustomFieldsValuesAttributes { get; set; }
	public object? CustomFieldsValues { get; set; }
	public object[]? Incidents { get; set; }
	public object[]? Problems { get; set; }
	public object[]? Changes { get; set; }
	public object[]? Solutions { get; set; }
	public object[]? Releases { get; set; }
	public object[]? PurchaseOrders { get; set; }
	public object[]? ConfigurationItems { get; set; }
	public int[]? ConfigurationItemIds { get; set; }
	public string[]? Cc { get; set; }
	public bool? IsServiceRequest { get; set; }
	public string? Origin { get; set; }
}
