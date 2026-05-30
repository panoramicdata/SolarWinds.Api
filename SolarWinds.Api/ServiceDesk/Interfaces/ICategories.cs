using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Categories API.
/// </summary>
public interface ICategories
{
	/// <summary>
	/// Gets a list of categories with query options.
	/// </summary>
	/// <param name="request">The query request.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of categories.</returns>
	[Get("/categories.json")]
	public Task<List<Category>> GetAsync([Query] GetCategoriesRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific category by ID.
	/// </summary>
	/// <param name="id">The ID of the category.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The category.</returns>
	[Get("/categories/{id}.json")]
	public Task<Category> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific category by ID with layout option.
	/// </summary>
	/// <param name="id">The ID of the category.</param>
	/// <param name="layout">The response layout.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The category.</returns>
	[Get("/categories/{id}.json")]
	public Task<Category> GetAsync(int id, [AliasAs("layout")] ResponseLayout? layout, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new category.
	/// </summary>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created category.</returns>
	[Post("/categories.json")]
	public Task<Category> CreateAsync([Body] CategoryCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing category.
	/// </summary>
	/// <param name="id">The ID of the category to update.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated category.</returns>
	[Put("/categories/{id}.json")]
	public Task<Category> UpdateAsync(int id, [Body] CategoryUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a category.
	/// </summary>
	/// <param name="id">The ID of the category to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/categories/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}