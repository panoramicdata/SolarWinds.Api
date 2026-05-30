namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable catalog item fields used by create and update operations.
/// </summary>
public sealed class CatalogItemWriteFields
{
	/// <summary>
	/// Gets or sets the catalog item name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets the catalog item description.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// Gets or sets category payload expected by the API.
	/// </summary>
	public object? Category { get; set; }
}
