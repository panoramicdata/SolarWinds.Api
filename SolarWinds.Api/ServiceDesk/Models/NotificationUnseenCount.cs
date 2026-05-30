namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents the unseen notification count returned by the notifications API.
/// </summary>
public class NotificationUnseenCount
{
	/// <summary>
	/// Gets or sets the number of unseen notifications.
	/// </summary>
	[JsonPropertyName("unseen_count")]
	public int UnseenCount { get; set; }
}
