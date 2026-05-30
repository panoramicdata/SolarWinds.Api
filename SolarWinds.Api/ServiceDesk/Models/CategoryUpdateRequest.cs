namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a category.
/// </summary>
public sealed class CategoryUpdateRequest
{
	/// <summary>
	/// Gets or sets the category fields to update.
	/// </summary>
	public CategoryWriteFields Category { get; set; } = new();
}
