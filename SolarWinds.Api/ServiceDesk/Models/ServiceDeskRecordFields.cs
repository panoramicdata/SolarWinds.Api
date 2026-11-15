namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Fields shared by Service Desk records that belong to a site and department.
/// </summary>
public abstract class ServiceDeskRecordFields
{
	/// <summary>
	/// Gets or sets the record title.
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
	/// Gets or sets the description.
	/// </summary>
	public string? Description { get; set; }
}
