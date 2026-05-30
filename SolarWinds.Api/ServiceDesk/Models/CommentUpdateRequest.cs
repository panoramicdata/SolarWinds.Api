namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class CommentUpdateRequest
{
	public CommentWriteFields Comment { get; set; } = new();
}
