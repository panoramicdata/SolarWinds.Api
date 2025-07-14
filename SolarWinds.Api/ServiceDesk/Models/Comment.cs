using System;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk comment.
/// </summary>
public class Comment
{
	/// <summary>
	/// Gets or sets the comment ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the comment body.
	/// </summary>
	public string Body { get; set; }

	/// <summary>
	/// Gets or sets the type of the commentable entity (e.g., "Incident").
	/// </summary>
	public string CommentableType { get; set; }

	/// <summary>
	/// Gets or sets the ID of the commentable entity.
	/// </summary>
	public int CommentableId { get; set; }

	/// <summary>
	/// Gets or sets whether the comment is public.
	/// </summary>
	public bool IsPublic { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}