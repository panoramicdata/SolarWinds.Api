namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a solution.
/// </summary>
public sealed class SolutionUpdateRequest
{
	/// <summary>
	/// Solution fields to update.
	/// </summary>
	public SolutionWriteFields Solution { get; set; } = new();
}
