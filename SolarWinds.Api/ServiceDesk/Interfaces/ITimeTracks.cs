using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Time Tracks API.
/// </summary>
public interface ITimeTracks
{
	/// <summary>
	/// Gets a list of time tracks for a source object.
	/// </summary>
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of time tracks.</returns>
	[Get("/{objectType}/{id}/time_tracks.json")]
	public Task<List<TimeTrack>> GetAllAsync(string objectType, int id, CancellationToken cancellationToken);

	/// <summary>
	/// Creates a new time track.
	/// </summary>
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="request">The create request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created time track.</returns>
	[Post("/{objectType}/{id}/time_tracks.json")]
	public Task<TimeTrack> CreateAsync(string objectType, int id, [Body] TimeTrackCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Updates an existing time track.
	/// </summary>
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="timeTrackId">The time track ID.</param>
	/// <param name="request">The update request payload.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The updated time track.</returns>
	[Put("/{objectType}/{id}/time_tracks/{timeTrackId}.json")]
	public Task<TimeTrack> UpdateAsync(string objectType, int id, int timeTrackId, [Body] TimeTrackUpdateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a time track.
	/// </summary>
	/// <param name="objectType">The source object type (for example, incidents).</param>
	/// <param name="id">The source object ID.</param>
	/// <param name="timeTrackId">The time track ID.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/{objectType}/{id}/time_tracks/{timeTrackId}.json")]
	public Task DeleteAsync(
		string objectType,
		int id,
		int timeTrackId,
		CancellationToken cancellationToken);
}