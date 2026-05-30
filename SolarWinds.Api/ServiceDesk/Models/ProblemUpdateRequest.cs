namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a problem.
/// </summary>
public sealed class ProblemUpdateRequest
{
	/// <summary>
	/// Problem fields to update.
	/// </summary>
	public ProblemWriteFields Problem { get; set; } = new();
}
