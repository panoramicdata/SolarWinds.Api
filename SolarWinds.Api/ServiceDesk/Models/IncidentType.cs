using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents an incident type entry returned by the incident_types API.
/// </summary>
public class IncidentType : BaseEntity
{
	/// <summary>
	/// Gets or sets the incident type name.
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the parent type ID.
	/// </summary>
	public int? ParentId { get; set; }

	/// <summary>
	/// Gets or sets the position/sort order.
	/// </summary>
	public int? Position { get; set; }

	/// <summary>
	/// Gets or sets the type label.
	/// </summary>
	public string? Label { get; set; }
}
