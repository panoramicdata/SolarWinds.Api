using Microsoft.Extensions.Logging;

namespace SolarWinds.Api;

/// <summary>
/// Options for configuring the SolarWindsOrionClient.
/// </summary>
public class SolarWindsOrionClientOptions
{
	/// <summary>
	/// Gets or sets the SolarWinds host name.
	/// </summary>
	public required string Hostname { get; set; }

	/// <summary>
	/// Gets or sets the account user name.
	/// </summary>
	public required string Username { get; set; }

	/// <summary>
	/// Gets or sets the account password.
	/// </summary>
	public required string Password { get; set; }

	/// <summary>
	/// Gets or sets the HTTPS port for the SWIS endpoint.
	/// </summary>
	public int Port { get; set; } = 17778;

	/// <summary>
	/// Gets or sets a value indicating whether to ignore TLS certificate validation errors.
	/// </summary>
	public bool IgnoreSslCertificateErrors { get; set; }

	/// <summary>
	/// Gets or sets the logger.
	/// </summary>
	public required ILogger Logger { get; set; }

	/// <summary>
	/// Gets or sets the HTTP proxy settings.
	/// </summary>
	public HttpProxySettings? ProxySettings { get; set; }
}
