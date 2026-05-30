namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a change.
/// </summary>
public sealed class ChangeCreateRequest
{
	/// <summary>
	/// Change fields to create.
	/// </summary>
	public ChangeWriteFields Change { get; set; } = new();
}
