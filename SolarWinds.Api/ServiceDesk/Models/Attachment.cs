using System;

namespace SolarWinds.Api.ServiceDesk.Models;
/// <summary>
/// Represents a Service Desk attachment.
/// </summary>
public class Attachment
{
	/// <summary>
	/// Gets or sets the attachment ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the attachment file name.
	/// </summary>
	public string FileName { get; set; }

	/// <summary>
	/// Gets or sets the attachment content type.
	/// </summary>
	public string ContentType { get; set; }

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
	public string AttachableType { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}