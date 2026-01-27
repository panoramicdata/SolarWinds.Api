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
	public required string Subject { get; set; }

	/// <summary>
	/// Gets or sets the ticket description.
	/// </summary>
	public required string Description { get; set; }

	/// <summary>
	/// Gets or sets the ticket status.
	/// </summary>
	public required string Status { get; set; }
}