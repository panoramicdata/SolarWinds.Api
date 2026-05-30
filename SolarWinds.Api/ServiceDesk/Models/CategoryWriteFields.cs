namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable category fields used by create and update operations.
/// </summary>
public sealed class CategoryWriteFields
{
	public string? Name { get; set; }
	public string? DefaultTags { get; set; }
	public int? ParentId { get; set; }
	public int? DefaultAssigneeId { get; set; }
	public int? DefaultGroupAssigneeId { get; set; }
	public bool? Deleted { get; set; }
}
