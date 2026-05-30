using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Comments API.
/// Note: The API documentation does not explicitly support retrieval of comments.
/// </summary>
public interface IComments
{
	/// <summary>
	/// Gets a list of comments for a specific object.
	/// </summary>
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="unmasked">Whether to return unmasked (unredacted) comment bodies.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of comments.</returns>
	[Get("/{objectType}/{id}/comments.json")]
	public Task<List<Comment>> GetAsync(ObjectType objectType, int id, [AliasAs("unmasked")] bool unmasked, CancellationToken cancellationToken);
	/// <summary>
	/// Creates a new comment.
	/// </summary>
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created comment.</returns>
	[Post("/{objectType}/{id}/comments.json")]
	public Task<Comment> CreateAsync(ObjectType objectType, int id, [Body] CommentCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing comment.
	/// </summary>
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="commentId">The comment ID.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated comment.</returns>
	[Put("/{objectType}/{id}/comments/{commentId}.json")]
	public Task<Comment> UpdateAsync(ObjectType objectType, int id, int commentId, [Body] CommentUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a comment.
	/// </summary>
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="commentId">The comment ID.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/{objectType}/{id}/comments/{commentId}.json")]
	public Task DeleteAsync(
		ObjectType objectType,
		int id,
		int commentId,
		CancellationToken cancellationToken);
}