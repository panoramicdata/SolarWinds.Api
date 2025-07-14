namespace SolarWinds.Api;

/// <summary>
/// Options for the SolarWinds Service Desk client.
/// </summary>
public class SolarWindsServiceDeskClientOptions
{
	/// <summary>
	/// The base URL for the SolarWinds Service Desk API (e.g., "https://api.samanage.com").
	/// </summary>
	public string BaseUrl { get; set; } = string.Empty;

	/// <summary>
	/// The access token for authentication.
	/// </summary>
	public string AccessToken { get; set; } = string.Empty;
}