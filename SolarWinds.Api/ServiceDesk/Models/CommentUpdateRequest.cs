namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for updating a comment.
/// </summary>
public sealed class CommentUpdateRequest
{
	/// <summary>
	/// Gets or sets the comment fields to update.
	/// </summary>
	public CommentWriteFields Comment { get; set; } = new();
}
