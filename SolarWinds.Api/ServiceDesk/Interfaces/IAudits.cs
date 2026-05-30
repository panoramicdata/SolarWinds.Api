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
	[Get("/audits.json")]
	public Task<List<Audit>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets audit records for a specific source object.
	/// </summary>
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of audit records.</returns>
	[Get("/{objectType}/{id}/audits.json")]
	public Task<List<Audit>> GetByObjectAsync(
		string objectType,
		int id,
		CancellationToken cancellationToken);
}