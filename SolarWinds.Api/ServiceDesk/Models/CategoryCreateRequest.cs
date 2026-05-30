namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a category.
/// </summary>
public sealed class CategoryCreateRequest
{
	public CategoryWriteFields Category { get; set; } = new();
}
