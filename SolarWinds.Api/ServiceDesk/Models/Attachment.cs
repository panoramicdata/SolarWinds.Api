using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk attachment.
/// </summary>
public class Attachment : TimestampedEntity
{
	/// <summary>
	/// Gets or sets the attachment file name.
	/// </summary>
	public string FileName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the attachment content type.
	/// </summary>
	public string ContentType { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the attachment file size.
	/// </summary>
	public long FileSize { get; set; }

	/// <summary>
	/// Gets or sets the ID of the attachable entity (e.g., Incident, Problem).
	/// </summary>
	public int AttachableId { get; set; }

	/// <summary>
	/// Gets or sets the type of the attachable entity (e.g., "Incident", "Problem").
	/// </summary>
	public string AttachableType { get; set; } = string.Empty;
}