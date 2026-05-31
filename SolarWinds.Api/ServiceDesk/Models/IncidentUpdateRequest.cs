namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating an incident.
/// </summary>
[JsonConverter(typeof(IncidentUpdateRequestJsonConverter))]
public sealed class IncidentUpdateRequest
{
	/// <summary>
	/// Incident fields to update.
	/// </summary>
	public IncidentUpdateFields Incident { get; set; } = new();
}
