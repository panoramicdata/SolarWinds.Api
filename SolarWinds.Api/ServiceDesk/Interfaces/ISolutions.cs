using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Solutions API.
/// </summary>
public interface ISolutions
{
	/// <summary>
	/// Gets a list of solutions.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of solutions.</returns>
	[Get("/solutions.json")]
	public Task<List<Solution>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a list of solutions using query parameters.
	/// </summary>
	/// <param name="request">The query parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of solutions.</returns>
	[Get("/solutions.json")]
	public Task<List<Solution>> GetAllAsync([Query] GetSolutionsRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific solution by ID.
	/// </summary>
	/// <param name="id">The ID of the solution.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The solution.</returns>
	[Get("/solutions/{id}.json")]
	public Task<Solution> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific solution by ID with layout option.
	/// </summary>
	/// <param name="id">The ID of the solution.</param>
	/// <param name="layout">The response layout.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The solution.</returns>
	[Get("/solutions/{id}.json")]
	public Task<Solution> GetAsync(int id, [AliasAs("layout")] ResponseLayout? layout, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new solution.
	/// </summary>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created solution.</returns>
	[Post("/solutions.json")]
	public Task<Solution> CreateAsync([Body] SolutionCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing solution.
	/// </summary>
	/// <param name="id">The ID of the solution to update.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated solution.</returns>
	[Put("/solutions/{id}.json")]
	public Task<Solution> UpdateAsync(int id, [Body] SolutionUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a solution.
	/// </summary>
	/// <param name="id">The ID of the solution to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/solutions/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}