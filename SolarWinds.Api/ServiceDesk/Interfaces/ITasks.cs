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
	/// Gets a list of tasks using query parameters.
	/// </summary>
	/// <param name="request">The task query parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of tasks.</returns>
	[Get("/tasks.json")]
	public Task<List<ServiceTask>> GetAsync([Query(CollectionFormat.Multi)] GetTasksRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new task.
	/// </summary>
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created task.</returns>
	[Post("/{objectType}/{id}/tasks.json")]
	public Task<ServiceTask> CreateAsync(ObjectType objectType, int id, [Body] TaskCreateRequest request, CancellationToken cancellationToken);

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
	public Task<ServiceTask> UpdateAsync(ObjectType objectType, int id, int taskId, [Body] TaskUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a task.
	/// </summary>
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="taskId">The task ID.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/{objectType}/{id}/tasks/{taskId}.json")]
	public Task DeleteAsync(
		ObjectType objectType,
		int id,
		int taskId,
		CancellationToken cancellationToken);
}