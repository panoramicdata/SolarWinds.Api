namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a release.
/// </summary>
public sealed class ReleaseUpdateRequest
{
	/// <summary>
	/// Release fields to update.
	/// </summary>
	public ReleaseWriteFields Release { get; set; } = new();
}
