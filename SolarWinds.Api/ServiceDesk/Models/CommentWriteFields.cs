namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Writable comment fields used by create and update operations.
/// </summary>
public sealed class CommentWriteFields
{
	/// <summary>
	/// Gets or sets the comment body text.
	/// </summary>
	public string? Body { get; set; }

	/// <summary>
	/// Gets or sets whether the comment is private/internal.
	/// </summary>
	public bool? IsPrivate { get; set; }

	/// <summary>
	/// Gets or sets the authoring user identifier.
	/// </summary>
	public int? UserId { get; set; }
}
