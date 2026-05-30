namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a time-track entry.
/// </summary>
public sealed class TimeTrackUpdateRequest
{
	/// <summary>
	/// Gets or sets the time-track fields to update.
	/// </summary>
	public TimeTrackWriteFields TimeTrack { get; set; } = new();
}
