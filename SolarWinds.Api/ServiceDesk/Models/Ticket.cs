namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk ticket.
/// </summary>
public class Ticket
{
	/// <summary>
	/// Gets or sets the ticket ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the ticket subject.
	/// </summary>
	public string Subject { get; set; }

	/// <summary>
	/// Gets or sets the ticket description.
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// Gets or sets the ticket status.
	/// </summary>
	public string Status { get; set; }
}