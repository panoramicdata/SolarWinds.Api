using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk service request.
/// </summary>
public class ServiceRequest : WorkItemEntity
{
	/// <summary>
	/// Gets or sets the site ID.
	/// </summary>
	public int SiteId { get; set; }

	/// <summary>
	/// Gets or sets the department ID.
	/// </summary>
	public int DepartmentId { get; set; }

	/// <summary>
	/// Gets or sets the priority.
	/// </summary>
	public string Priority { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the category.
	/// </summary>
	public object? Category { get; set; }

	/// <summary>
	/// Gets or sets the subcategory.
	/// </summary>
	public object? Subcategory { get; set; }
}