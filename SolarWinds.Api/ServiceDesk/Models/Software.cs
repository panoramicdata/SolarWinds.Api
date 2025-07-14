using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk software asset.
/// </summary>
public class Software
{
	/// <summary>
	/// Gets or sets the software ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the software name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the software version.
	/// </summary>
	public string Version { get; set; }

	/// <summary>
	/// Gets or sets the software license key.
	/// </summary>
	public string LicenseKey { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}