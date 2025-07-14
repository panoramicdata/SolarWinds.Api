using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using SolarWinds.Api.ServiceDesk.Models;
using ServiceTask = SolarWinds.Api.ServiceDesk.Models.ServiceTask;

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
	[Get("/api/v2/solutions.json")]
	Task<List<Solution>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific solution by ID.
	/// </summary>
	/// <param name="id">The ID of the solution.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The solution.</returns>
	[Get("/api/v2/solutions/{id}.json")]
	Task<Solution> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new solution.
	/// </summary>
	/// <param name="solution">The solution to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created solution.</returns>
	[Post("/api/v2/solutions.json")]
	Task<Solution> CreateAsync([Body] Solution solution, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing solution.
	/// </summary>
	/// <param name="id">The ID of the solution to update.</param>
	/// <param name="solution">The solution data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated solution.</returns>
	[Put("/api/v2/solutions/{id}.json")]
	Task<Solution> UpdateAsync(int id, [Body] Solution solution, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a solution.
	/// </summary>
	/// <param name="id">The ID of the solution to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/solutions/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}