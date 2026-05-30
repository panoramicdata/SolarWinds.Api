namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a category.
/// </summary>
public sealed class CategoryCreateRequest
{
	/// <summary>
	/// Gets or sets the category payload to create.
	/// </summary>
	public CategoryWriteFields Category { get; set; } = new();
}
