namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class CommentCreateRequest
{
	public CommentWriteFields Comment { get; set; } = new();
}
