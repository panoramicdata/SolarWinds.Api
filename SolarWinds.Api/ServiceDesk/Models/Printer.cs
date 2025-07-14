using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk printer asset.
/// </summary>
public class Printer
{
	/// <summary>
	/// Gets or sets the printer ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the printer name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the asset tag.
	/// </summary>
	public string AssetTag { get; set; }

	/// <summary>
	/// Gets or sets the IP address.
	/// </summary>
	public string IpAddress { get; set; }

	/// <summary>
	/// Gets or sets custom field values.
	/// </summary>
	public object CustomFieldsValues { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}