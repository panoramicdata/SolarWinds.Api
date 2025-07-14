using System;
using System.Collections.Generic;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk Configuration Item (CI).
/// </summary>
public class ConfigurationItem
{
	/// <summary>
	/// Gets or sets the CI ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the CI name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the CI description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the CI type.
	/// </summary>
	public string Type { get; set; }

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

	/// <summary>
	/// Gets or sets the list of dependent asset IDs.
	/// </summary>
	public List<int> DependentAssetIds { get; set; }
}