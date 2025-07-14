using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Departments API.
/// </summary>
public interface IDepartments
{
	/// <summary>
	/// Gets a list of departments.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of departments.</returns>
	[Get("/api/v2/departments.json")]
	Task<List<Department>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific department by ID.
	/// </summary>
	/// <param name="id">The ID of the department.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The department.</returns>
	[Get("/api/v2/departments/{id}.json")]
	Task<Department> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new department.
	/// </summary>
	/// <param name="department">The department to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created department.</returns>
	[Post("/api/v2/departments.json")]
	Task<Department> CreateAsync([Body] Department department, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing department.
	/// </summary>
	/// <param name="id">The ID of the department to update.</param>
	/// <param name="department">The department data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated department.</returns>
	[Put("/api/v2/departments/{id}.json")]
	Task<Department> UpdateAsync(int id, [Body] Department department, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a department.
	/// </summary>
	/// <param name="id">The ID of the department to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/departments/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}