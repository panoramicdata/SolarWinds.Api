using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk hardware asset.
/// </summary>
public class Hardware : NamedEntity
{
	/// <summary>
	/// Gets or sets the asset tag.
	/// </summary>
	public required string AssetTag { get; set; }

	/// <summary>
	/// Gets or sets the IP address.
	/// </summary>
	public required string IpAddress { get; set; }
}