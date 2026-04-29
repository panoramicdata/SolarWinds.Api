using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk printer asset.
/// </summary>
public class Printer : NamedEntity
{
	/// <summary>
	/// Gets or sets the asset tag.
	/// </summary>
	public string AssetTag { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the IP address.
	/// </summary>
	public string IpAddress { get; set; } = string.Empty;
}