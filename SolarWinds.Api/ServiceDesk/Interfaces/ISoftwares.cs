using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Software API.
/// Note: The API documentation only supports retrieval of software records.
/// </summary>
public interface ISoftwares
{
	/// <summary>
	/// Gets a list of software records.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of software records.</returns>
	[Get("/api/v2/softwares.json")]
	Task<List<Software>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific software record by ID.
	/// </summary>
	/// <param name="id">The ID of the software record.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The software record.</returns>
	[Get("/api/v2/softwares/{id}.json")]
	public Task<Software> GetAsync(
		int id,
		CancellationToken cancellationToken);
}