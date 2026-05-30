namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a service request from a catalog item.
/// </summary>
public sealed class ServiceRequestCreateRequest
{
	/// <summary>
	/// Incident fields used to create the service request.
	/// </summary>
	public IncidentWriteFields Incident { get; set; } = new();
}
