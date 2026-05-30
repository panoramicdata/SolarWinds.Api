namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a solution.
/// </summary>
public sealed class SolutionCreateRequest
{
	/// <summary>
	/// Solution fields to create.
	/// </summary>
	public SolutionWriteFields Solution { get; set; } = new();
}
