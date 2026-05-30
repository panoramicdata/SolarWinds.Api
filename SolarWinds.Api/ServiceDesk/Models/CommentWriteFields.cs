namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class CommentWriteFields
{
	public string? Body { get; set; }
	public bool? IsPrivate { get; set; }
	public int? UserId { get; set; }
}
