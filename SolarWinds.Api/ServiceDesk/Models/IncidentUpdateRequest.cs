namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating an incident.
/// </summary>
public sealed class IncidentUpdateRequest
{
	/// <summary>
	/// Incident fields to update.
	/// </summary>
	public IncidentWriteFields Incident { get; set; } = new();
}
