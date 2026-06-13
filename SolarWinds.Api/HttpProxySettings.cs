namespace SolarWinds.Api;

/// <summary>
/// Represents proxy settings for HTTP requests.
/// </summary>
public class HttpProxySettings
{
	/// <summary>
	/// Gets or sets the proxy server URI.
	/// </summary>
	public required Uri Uri { get; set; }

	/// <summary>
	/// Gets or sets the username for proxy authentication.
	/// </summary>
	public string? Username { get; set; }

	/// <summary>
	/// Gets or sets the password for proxy authentication.
	/// </summary>
	public string? Password { get; set; }
}
