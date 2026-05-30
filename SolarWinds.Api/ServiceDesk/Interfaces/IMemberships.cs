using Refit;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for Service Desk memberships endpoints.
/// </summary>
public interface IMemberships
{
	/// <summary>
	/// Creates group memberships for one or more users.
	/// </summary>
	/// <param name="groupId">The group ID.</param>
	/// <param name="userIds">Comma-separated user IDs.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Post("/memberships.json")]
	public Task CreateAsync(
		[AliasAs("group_id")] int groupId,
		[AliasAs("user_ids")] string userIds,
		CancellationToken cancellationToken);

	/// <summary>
	/// Deletes a membership.
	/// </summary>
	/// <param name="id">The membership ID.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	[Delete("/memberships/{id}.json")]
	public Task DeleteAsync(int id, CancellationToken cancellationToken);
}
