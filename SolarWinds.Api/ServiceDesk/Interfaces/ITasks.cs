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
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created task.</returns>
	[Post("/{objectType}/{id}/tasks.json")]
	public Task<ServiceTask> CreateAsync(string objectType, int id, [Body] TaskCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing task.
	/// </summary>
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="taskId">The task ID.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated task.</returns>
	[Put("/{objectType}/{id}/tasks/{taskId}.json")]
	public Task<ServiceTask> UpdateAsync(string objectType, int id, int taskId, [Body] TaskUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a task.
	/// </summary>
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="taskId">The task ID.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/{objectType}/{id}/tasks/{taskId}.json")]
	public Task DeleteAsync(
		string objectType,
		int id,
		int taskId,
		CancellationToken cancellationToken);
}