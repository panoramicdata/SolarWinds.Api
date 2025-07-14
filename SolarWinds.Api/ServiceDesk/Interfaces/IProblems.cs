using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Problems API.
/// </summary>
public interface IProblems
{
	/// <summary>
	/// Gets a list of problems.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of problems.</returns>
	[Get("/api/v2/problems.json")]
	Task<List<Problem>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific problem by ID.
	/// </summary>
	/// <param name="id">The ID of the problem.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The problem.</returns>
	[Get("/api/v2/problems/{id}.json")]
	Task<Problem> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new problem.
	/// </summary>
	/// <param name="problem">The problem to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created problem.</returns>
	[Post("/api/v2/problems.json")]
	Task<Problem> CreateAsync([Body] Problem problem, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing problem.
	/// </summary>
	/// <param name="id">The ID of the problem to update.</param>
	/// <param name="problem">The problem data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated problem.</returns>
	[Put("/api/v2/problems/{id}.json")]
	Task<Problem> UpdateAsync(int id, [Body] Problem problem, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a problem.
	/// </summary>
	/// <param name="id">The ID of the problem to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/problems/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}