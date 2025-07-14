using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Printers API.
/// Note: The API documentation does not explicitly support creation or deletion of printers.
/// </summary>
public interface IPrinters
{
	/// <summary>
	/// Gets a list of printers.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of printers.</returns>
	[Get("/api/v2/printers.json")]
	Task<List<Printer>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific printer by ID.
	/// </summary>
	/// <param name="id">The ID of the printer.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The printer.</returns>
	[Get("/api/v2/printers/{id}.json")]
	Task<Printer> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing printer.
	/// </summary>
	/// <param name="id">The ID of the printer to update.</param>
	/// <param name="printer">The printer data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated printer.</returns>
	[Put("/api/v2/printers/{id}.json")]
	public Task<Printer> UpdateAsync(
		int id,
		[Body] Printer printer,
		CancellationToken cancellationToken);
}