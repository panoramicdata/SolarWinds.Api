namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a time-track entry.
/// </summary>
public sealed class TimeTrackCreateRequest
{
	/// <summary>
	/// Gets or sets the time-track payload to create.
	/// </summary>
	public TimeTrackWriteFields TimeTrack { get; set; } = new();
}
