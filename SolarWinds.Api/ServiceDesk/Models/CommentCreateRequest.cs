namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Request payload for creating a comment.
/// </summary>
public sealed class CommentCreateRequest
{
	/// <summary>
	/// Gets or sets the comment fields to create.
	/// </summary>
	public CommentWriteFields Comment { get; set; } = new();
}
