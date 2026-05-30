namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable solution fields used by create and update operations.
/// </summary>
public sealed class SolutionWriteFields
{
	public string? Name { get; set; }
	public int? SiteId { get; set; }
	public int? DepartmentId { get; set; }
	public string? Description { get; set; }
	public string? State { get; set; }
	public object? Category { get; set; }
	public object? Subcategory { get; set; }
	public string? AddToTagList { get; set; }
	public string? RemoveFromTagList { get; set; }
	public string? TagList { get; set; }
	public object[]? CustomFieldsValuesAttributes { get; set; }
	public object? CustomFieldsValues { get; set; }
	public int[]? IncidentIds { get; set; }
}
