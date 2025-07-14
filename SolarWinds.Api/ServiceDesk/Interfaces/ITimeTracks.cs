using Refit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Time Tracks API.
/// </summary>
public interface ITimeTracks
{
	/// <summary>
	/// Gets a list of time tracks.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of time tracks.</returns>
	[Get("/api/v2/time_tracks.json")]
	Task<List<TimeTrack>> GetAllAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Gets a specific time track by ID.
	/// </summary>
	/// <param name="id">The ID of the time track.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The time track.</returns>
	[Get("/api/v2/time_tracks/{id}.json")]
	Task<TimeTrack> GetAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new time track.
	/// </summary>
	/// <param name="timeTrack">The time track to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created time track.</returns>
	[Post("/api/v2/time_tracks.json")]
	Task<TimeTrack> CreateAsync([Body] TimeTrack timeTrack, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing time track.
	/// </summary>
	/// <param name="id">The ID of the time track to update.</param>
	/// <param name="timeTrack">The time track data to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated time track.</returns>
	[Put("/api/v2/time_tracks/{id}.json")]
	Task<TimeTrack> UpdateAsync(int id, [Body] TimeTrack timeTrack, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a time track.
	/// </summary>
	/// <param name="id">The ID of the time track to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/api/v2/time_tracks/{id}.json")]
	public Task DeleteAsync(
		int id,
		CancellationToken cancellationToken);
}