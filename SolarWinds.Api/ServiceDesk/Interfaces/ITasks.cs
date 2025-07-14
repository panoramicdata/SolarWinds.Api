using System.Threading;
using System.Threading.Tasks;
using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Tasks API.
/// Note: The API documentation does not explicitly support retrieval of tasks.
/// </summary>
public interface ITasks
{
	/// <summary>
	/// Creates a new task.
	/// </summary>
	/// <param name="task">The task to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created task.</returns>
	[Post("/api/v2/tasks.json")]
	Task<ServiceTask> CreateAsync([Body] ServiceTask task, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing task.
	/// </summary>
	/// <param name="id">The ID of the task to update.</param>
	/// <param name="task">The task data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated task.</returns>
	[Put("/api/v2/tasks/{id}.json")]
	Task<ServiceTask> UpdateAsync(int id, [Body] ServiceTask task, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a task.
	/// </summary>
	/// <param name="id">The ID of the task to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/tasks/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}