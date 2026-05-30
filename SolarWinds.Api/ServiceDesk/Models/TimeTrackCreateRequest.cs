namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class TimeTrackCreateRequest
{
	public TimeTrackWriteFields TimeTrack { get; set; } = new();
}
