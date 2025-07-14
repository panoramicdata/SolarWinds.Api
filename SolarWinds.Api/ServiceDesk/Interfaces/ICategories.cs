using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Categories API.
/// </summary>
public interface ICategories
{
	/// <summary>
	/// Gets a list of categories.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of categories.</returns>
	[Get("/api/v2/categories.json")]
	Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific category by ID.
	/// </summary>
	/// <param name="id">The ID of the category.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The category.</returns>
	[Get("/api/v2/categories/{id}.json")]
	Task<Category> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new category.
	/// </summary>
	/// <param name="category">The category to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created category.</returns>
	[Post("/api/v2/categories.json")]
	Task<Category> CreateAsync([Body] Category category, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing category.
	/// </summary>
	/// <param name="id">The ID of the category to update.</param>
	/// <param name="category">The category data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated category.</returns>
	[Put("/api/v2/categories/{id}.json")]
	Task<Category> UpdateAsync(int id, [Body] Category category, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a category.
	/// </summary>
	/// <param name="id">The ID of the category to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/categories/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}