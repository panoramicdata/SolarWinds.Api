using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk service request.
/// </summary>
public class ServiceRequest
{
	/// <summary>
	/// Gets or sets the service request ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the service request name/subject.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the service request description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the site ID.
	/// </summary>
	public int SiteId { get; set; }

	/// <summary>
	/// Gets or sets the department ID.
	/// </summary>
	public int DepartmentId { get; set; }

	/// <summary>
	/// Gets or sets the requester's email.
	/// </summary>
	public string Requester { get; set; }

	/// <summary>
	/// Gets or sets the priority.
	/// </summary>
	public string Priority { get; set; }

	/// <summary>
	/// Gets or sets the category.
	/// </summary>
	public string Category { get; set; }

	/// <summary>
	/// Gets or sets the subcategory.
	/// </summary>
	public string Subcategory { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}