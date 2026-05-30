using Refit;
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
	[Get("/problems.json")]
	public Task<List<Problem>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a list of problems using query parameters.
	/// </summary>
	/// <param name="request">The query parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of problems.</returns>
	[Get("/problems.json")]
	public Task<List<Problem>> GetAllAsync([Query] GetProblemsRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific problem by ID.
	/// </summary>
	/// <param name="id">The ID of the problem.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The problem.</returns>
	[Get("/problems/{id}.json")]
	public Task<Problem> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific problem by ID with layout option.
	/// </summary>
	/// <param name="id">The ID of the problem.</param>
	/// <param name="layout">The response layout.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The problem.</returns>
	[Get("/problems/{id}.json")]
	public Task<Problem> GetAsync(int id, [AliasAs("layout")] ResponseLayout? layout, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new problem.
	/// </summary>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created problem.</returns>
	[Post("/problems.json")]
	public Task<Problem> CreateAsync([Body] ProblemCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing problem.
	/// </summary>
	/// <param name="id">The ID of the problem to update.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated problem.</returns>
	[Put("/problems/{id}.json")]
	public Task<Problem> UpdateAsync(int id, [Body] ProblemUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a problem.
	/// </summary>
	/// <param name="id">The ID of the problem to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/problems/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}