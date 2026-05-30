namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable incident fields used by create and update operations.
/// </summary>
public sealed class IncidentWriteFields
{
	/// <summary>
	/// Gets or sets incident title.
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
	/// Gets or sets rich-text incident description.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets plain-text incident description.
	/// </summary>
	public string? DescriptionNoHtml { get; set; }

	/// <summary>
	/// Gets or sets state identifier.
	/// </summary>
	public int? StateId { get; set; }

	/// <summary>
	/// Gets or sets state value.
	/// </summary>
	public string? State { get; set; }

	/// <summary>
	/// Gets or sets assignee payload.
	/// </summary>
	public object? Assignee { get; set; }

	/// <summary>
	/// Gets or sets group-assignee payload.
	/// </summary>
	public object? GroupAssignee { get; set; }

	/// <summary>
	/// Gets or sets requester payload.
	/// </summary>
	public object? Requester { get; set; }

	/// <summary>
	/// Gets or sets incident priority.
	/// </summary>
	public string? Priority { get; set; }

	/// <summary>
	/// Gets or sets category payload.
	/// </summary>
	public object? Category { get; set; }

	/// <summary>
	/// Gets or sets subcategory payload.
	/// </summary>
	public object? Subcategory { get; set; }

	/// <summary>
	/// Gets or sets incident due date/time.
	/// </summary>
	public DateTime? DueAt { get; set; }

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
	/// Gets or sets related incident link payload.
	/// </summary>
	public object[]? Incidents { get; set; }

	/// <summary>
	/// Gets or sets related problem link payload.
	/// </summary>
	public object[]? Problems { get; set; }

	/// <summary>
	/// Gets or sets related change link payload.
	/// </summary>
	public object[]? Changes { get; set; }

	/// <summary>
	/// Gets or sets related solution link payload.
	/// </summary>
	public object[]? Solutions { get; set; }

	/// <summary>
	/// Gets or sets related release link payload.
	/// </summary>
	public object[]? Releases { get; set; }

	/// <summary>
	/// Gets or sets related purchase-order link payload.
	/// </summary>
	public object[]? PurchaseOrders { get; set; }

	/// <summary>
	/// Gets or sets related configuration-item link payload.
	/// </summary>
	public object[]? ConfigurationItems { get; set; }

	/// <summary>
	/// Gets or sets related configuration-item identifiers.
	/// </summary>
	public int[]? ConfigurationItemIds { get; set; }

	/// <summary>
	/// Gets or sets CC email recipients.
	/// </summary>
	public string[]? Cc { get; set; }

	/// <summary>
	/// Gets or sets whether the incident is a service request.
	/// </summary>
	public bool? IsServiceRequest { get; set; }

	/// <summary>
	/// Gets or sets incident origin channel.
	/// </summary>
	public string? Origin { get; set; }
}
