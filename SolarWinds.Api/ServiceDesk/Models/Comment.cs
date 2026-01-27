using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk comment.
/// </summary>
public class Comment : TimestampedEntity
{
	/// <summary>
	/// Gets or sets the comment body.
	/// </summary>
	public required string Body { get; set; }

	/// <summary>
	/// Gets or sets the type of the commentable entity (e.g., "Incident").
	/// </summary>
	public required string CommentableType { get; set; }

	/// <summary>
	/// Gets or sets the ID of the commentable entity.
	/// </summary>
	public int CommentableId { get; set; }

	/// <summary>
	/// Gets or sets whether the comment is public.
	/// </summary>
	public bool IsPublic { get; set; }
}