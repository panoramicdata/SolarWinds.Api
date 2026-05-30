namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a release.
/// </summary>
public sealed class ReleaseCreateRequest
{
	/// <summary>
	/// Release fields to create.
	/// </summary>
	public ReleaseWriteFields Release { get; set; } = new();
}
