using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Risks API.
/// Note: The API documentation only supports retrieval of risk records.
/// </summary>
public interface IRisks
{
	/// <summary>
	/// Gets a list of risk records.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of risk records.</returns>
	[Get("/api/v2/risks.json")]
	Task<List<Risk>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific risk record by ID.
	/// </summary>
	/// <param name="id">The ID of the risk record.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The risk record.</returns>
	[Get("/api/v2/risks/{id}.json")]
	public Task<Risk> GetAsync(
		int id,
		CancellationToken cancellationToken);
}