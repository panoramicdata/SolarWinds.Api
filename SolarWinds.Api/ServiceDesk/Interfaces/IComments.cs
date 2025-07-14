using Refit;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Comments API.
/// Note: The API documentation does not explicitly support retrieval of comments.
/// </summary>
public interface IComments
{
	/// <summary>
	/// Creates a new comment.
	/// </summary>
	/// <param name="comment">The comment to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created comment.</returns>
	[Post("/api/v2/comments.json")]
	Task<Comment> CreateAsync([Body] Comment comment, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing comment.
	/// </summary>
	/// <param name="id">The ID of the comment to update.</param>
	/// <param name="comment">The comment data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated comment.</returns>
	[Put("/api/v2/comments/{id}.json")]
	Task<Comment> UpdateAsync(int id, [Body] Comment comment, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a comment.
	/// </summary>
	/// <param name="id">The ID of the comment to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/comments/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}