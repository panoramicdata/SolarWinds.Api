namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a change.
/// </summary>
public sealed class ChangeUpdateRequest
{
	/// <summary>
	/// Change fields to update.
	/// </summary>
	public ChangeWriteFields Change { get; set; } = new();
}
