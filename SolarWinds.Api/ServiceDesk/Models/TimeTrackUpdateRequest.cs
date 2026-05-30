namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class TimeTrackUpdateRequest
{
	public TimeTrackWriteFields TimeTrack { get; set; } = new();
}
