namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating an incident.
/// </summary>
public sealed class IncidentCreateRequest
{
	/// <summary>
	/// Incident fields to create.
	/// </summary>
	public IncidentWriteFields Incident { get; set; } = new();
}
