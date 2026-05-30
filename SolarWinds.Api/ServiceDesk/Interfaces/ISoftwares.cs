using Refit;
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
	/// <param name="request">The software query parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of software records.</returns>
	[Get("/softwares.json")]
	public Task<List<Software>> GetAsync([Query(CollectionFormat.Multi)] GetSoftwaresRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Gets a list of software records.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of software records.</returns>
	[Get("/softwares.json")]
	public Task<List<Software>> GetAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific software record by ID.
	/// </summary>
	/// <param name="id">The ID of the software record.</param>
	/// <param name="layout">The response layout.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The software record.</returns>
	[Get("/softwares/{id}.json")]
	public Task<Software> GetAsync(
		int id,
		[AliasAs("layout")] ResponseLayout layout,
		CancellationToken cancellationToken);
}