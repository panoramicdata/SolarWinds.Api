using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk catalog item.
/// </summary>
public class CatalogItem : DescribedEntity
{
	/// <summary>
	/// Gets or sets the catalog item category payload.
	/// </summary>
	public object? Category { get; set; }
}