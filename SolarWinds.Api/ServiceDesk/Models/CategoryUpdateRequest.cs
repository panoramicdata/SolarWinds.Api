namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a category.
/// </summary>
public sealed class CategoryUpdateRequest
{
	public CategoryWriteFields Category { get; set; } = new();
}
