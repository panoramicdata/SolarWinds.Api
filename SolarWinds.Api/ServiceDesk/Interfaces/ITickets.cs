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
[Get("/tickets.json")]
public Task<List<Ticket>> GetAllAsync(CancellationToken cancellationToken);

/// <summary>
/// Gets a specific ticket by ID.
/// </summary>
/// <param name="id">The ID of the ticket.</param>
/// <param name="cancellationToken">The cancellation token.</param>
/// <returns>The ticket.</returns>
[Get("/tickets/{id}.json")]
public Task<Ticket> GetAsync(
int id,
CancellationToken cancellationToken);

/// <summary>
/// Creates a new ticket.
/// </summary>
/// <param name="ticket">The ticket to create.</param>
/// <param name="cancellationToken">The cancellation token.</param>
/// <returns>The created ticket.</returns>
[Post("/tickets.json")]
public Task<Ticket> CreateAsync(
[Body] Ticket ticket,
CancellationToken cancellationToken);

/// <summary>
/// Updates an existing ticket.
/// </summary>
/// <param name="id">The ID of the ticket to update.</param>
/// <param name="ticket">The ticket data to update.</param>
/// <param name="cancellationToken">The cancellation token.</param>
/// <returns>The updated ticket.</returns>
[Put("/tickets/{id}.json")]
public Task<Ticket> UpdateAsync(
int id,
[Body] Ticket ticket,
CancellationToken cancellationToken);

/// <summary>
/// Deletes a ticket.
/// </summary>
/// <param name="id">The ID of the ticket to delete.</param>
/// <param name="cancellationToken">The cancellation token.</param>
[Delete("/tickets/{id}.json")]
public Task DeleteAsync(
int id,
CancellationToken cancellationToken);
}