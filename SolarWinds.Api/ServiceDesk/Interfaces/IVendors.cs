using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Vendors API.
/// </summary>
public interface IVendors
{
	/// <summary>
	/// Gets a list of vendors.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of vendors.</returns>
	[Get("/api/v2/vendors.json")]
	Task<List<Vendor>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific vendor by ID.
	/// </summary>
	/// <param name="id">The ID of the vendor.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The vendor.</returns>
	[Get("/api/v2/vendors/{id}.json")]
	Task<Vendor> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new vendor.
	/// </summary>
	/// <param name="vendor">The vendor to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created vendor.</returns>
	[Post("/api/v2/vendors.json")]
	Task<Vendor> CreateAsync([Body] Vendor vendor, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing vendor.
	/// </summary>
	/// <param name="id">The ID of the vendor to update.</param>
	/// <param name="vendor">The vendor data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated vendor.</returns>
	[Put("/api/v2/vendors/{id}.json")]
	Task<Vendor> UpdateAsync(int id, [Body] Vendor vendor, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a vendor.
	/// </summary>
	/// <param name="id">The ID of the vendor to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/vendors/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}