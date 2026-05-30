namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable time-track fields used by create and update operations.
/// </summary>
public sealed class TimeTrackWriteFields
{
	/// <summary>
	/// Gets or sets time-track title.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets parsed duration text (for example, "1h 30m").
	/// </summary>
	public string? MinutesParsed { get; set; }
}
