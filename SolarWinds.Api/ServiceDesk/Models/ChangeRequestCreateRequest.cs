namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a change request from a change catalog.
/// </summary>
public sealed class ChangeRequestCreateRequest
{
	/// <summary>
	/// Change request fields to create.
	/// </summary>
	public ChangeRequestWriteFields ChangeRequest { get; set; } = new();
}
