using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Notifications API.
/// </summary>
public interface INotifications
{
	/// <summary>
	/// Gets the count of unseen notifications for the current user.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The unseen notification count.</returns>
	[Get("/notifications/unseen_count.json")]
	public Task<NotificationUnseenCount> GetUnseenCountAsync(CancellationToken cancellationToken);
}
