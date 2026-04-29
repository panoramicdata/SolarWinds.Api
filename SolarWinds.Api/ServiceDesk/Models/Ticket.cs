using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk ticket.
/// </summary>
public class Ticket : BaseEntity
{
	/// <summary>
	/// Gets or sets the ticket subject.
	/// </summary>
	public string Subject { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the ticket description.
	/// </summary>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the ticket status.
	/// </summary>
	public string Status { get; set; } = string.Empty;
}