using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;
/// <summary>
/// Interface for the Service Desk Audits API.
/// Note: The API documentation only supports retrieval of audit records.
/// </summary>
public interface IAudits
{
	/// <summary>
	/// Gets a list of audit records.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of audit records.</returns>
	[Get("/api/v2/audits.json")]
	public Task<List<Audit>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific audit record by ID.
	/// </summary>
	/// <param name="id">The ID of the audit record.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The audit record.</returns>
	[Get("/api/v2/audits/{id}.json")]
	public Task<Audit> GetAsync(
		int id,
		CancellationToken cancellationToken);
}