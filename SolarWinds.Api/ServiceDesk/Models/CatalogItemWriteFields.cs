namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable catalog item fields used by create and update operations.
/// </summary>
public sealed class CatalogItemWriteFields
{
	public string? Name { get; set; }
	public string? Description { get; set; }
	public object? Category { get; set; }
}
