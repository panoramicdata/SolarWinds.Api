using System;
using System.Collections.Generic;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk catalog item.
/// </summary>
public class CatalogItem
{
	/// <summary>
	/// Gets or sets the catalog item ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the catalog item name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the catalog item description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the catalog item category.
	/// </summary>
	public string Category { get; set; }

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