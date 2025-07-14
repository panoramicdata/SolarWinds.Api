using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Tickets API.
/// </summary>
public interface ITickets
{
	/// <summary>
	/// Gets a list of tickets.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of tickets.</returns>
	[Get("/api/v2/tickets.json")]
	public Task<List<Ticket>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific ticket by ID.
	/// </summary>
	/// <param name="id">The ID of the ticket.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The ticket.</returns>
	[Get("/api/v2/tickets/{id}.json")]
	public Task<Ticket> GetAsync(
		int id,
		CancellationToken cancellationToken);
}