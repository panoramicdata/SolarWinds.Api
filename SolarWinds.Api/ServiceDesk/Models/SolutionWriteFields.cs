namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable solution fields used by create and update operations.
/// </summary>
public sealed class SolutionWriteFields
{
	/// <summary>
	/// Gets or sets solution title.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets site identifier.
	/// </summary>
	public int? SiteId { get; set; }

	/// <summary>
	/// Gets or sets department identifier.
	/// </summary>
	public int? DepartmentId { get; set; }

	/// <summary>
	/// Gets or sets solution description.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets solution state.
	/// </summary>
	public string? State { get; set; }

	/// <summary>
	/// Gets or sets solution category payload.
	/// </summary>
	public object? Category { get; set; }

	/// <summary>
	/// Gets or sets solution subcategory payload.
	/// </summary>
	public object? Subcategory { get; set; }

	/// <summary>
	/// Gets or sets tags to append.
	/// </summary>
	public string? AddToTagList { get; set; }

	/// <summary>
	/// Gets or sets tags to remove.
	/// </summary>
	public string? RemoveFromTagList { get; set; }

	/// <summary>
	/// Gets or sets full replacement tag list.
	/// </summary>
	public string? TagList { get; set; }

	/// <summary>
	/// Gets or sets custom field value attribute payload.
	/// </summary>
	public object[]? CustomFieldsValuesAttributes { get; set; }

	/// <summary>
	/// Gets or sets custom field value payload.
	/// </summary>
	public object? CustomFieldsValues { get; set; }

	/// <summary>
	/// Gets or sets linked incident identifiers.
	/// </summary>
	public int[]? IncidentIds { get; set; }
}
