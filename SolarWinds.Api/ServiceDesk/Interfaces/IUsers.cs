using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Users API.
/// </summary>
public interface IUsers
{
	/// <summary>
	/// Gets a list of users.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of users.</returns>
	[Get("/users.json")]
	public Task<List<User>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific user by ID.
	/// </summary>
	/// <param name="id">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The user.</returns>
	[Get("/users/{id}.json")]
	public Task<User> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new user.
	/// </summary>
	/// <param name="user">The user to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created user.</returns>
	[Post("/users.json")]
	public Task<User> CreateAsync([Body] User user, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing user.
	/// </summary>
	/// <param name="id">The ID of the user to update.</param>
	/// <param name="user">The user data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated user.</returns>
	[Put("/users/{id}.json")]
	public Task<User> UpdateAsync(
		int id,
		[Body] User user,
		CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a user.
	/// </summary>
	/// <param name="id">The ID of the user to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/users/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}