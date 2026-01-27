using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk risk.
/// </summary>
public class Risk : DescribedEntity
{
	/// <summary>
	/// Gets or sets the risk level.
	/// </summary>
	public required string Level { get; set; }
}