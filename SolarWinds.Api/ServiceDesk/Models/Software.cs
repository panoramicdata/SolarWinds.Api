using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk software asset.
/// </summary>
public class Software : NamedEntity
{
	/// <summary>
	/// Gets or sets the software version.
	/// </summary>
	public string Version { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the software license key.
	/// </summary>
	public string LicenseKey { get; set; } = string.Empty;
}