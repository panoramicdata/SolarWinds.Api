namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a problem.
/// </summary>
public sealed class ProblemCreateRequest
{
	/// <summary>
	/// Problem fields to create.
	/// </summary>
	public ProblemWriteFields Problem { get; set; } = new();
}
