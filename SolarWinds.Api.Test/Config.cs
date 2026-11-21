using System.Text.Json.Serialization;

namespace SolarWinds.Api.Test;

/// <summary>
/// Test config
/// </summary>
public class Config
{
	/// <summary>
	/// The hostname
	/// </summary>
	[JsonPropertyName("Hostname")]
	public string Hostname { get; set; } = string.Empty;

	/// <summary>
	/// The username
	/// </summary>
	[JsonPropertyName("Username")]
	public string Username { get; set; } = string.Empty;

	/// <summary>
	/// The password
	/// </summary>
	[JsonPropertyName("Password")]
	public string Password { get; set; } = string.Empty;

	/// <summary>
	/// The port
	/// </summary>
	[JsonPropertyName("Port")]
	public int Port { get; set; }

	/// <summary>
	/// Whether to ignore SSL Certificate Errors
	/// </summary>
	[JsonPropertyName("IgnoreSslCertificateErrors")]
	public bool IgnoreSslCertificateErrors { get; set; }

	/// <summary>
	/// Configuration for the SolarWinds Service Desk API
	/// </summary>
	[JsonPropertyName("ServiceDesk")]
	public ServiceDeskConfig ServiceDesk { get; set; } = new();
}

/// <summary>
/// Service Desk API configuration
/// </summary>
public class ServiceDeskConfig
{
	/// <summary>
	/// The base URL for the Service Desk API (e.g. https://api.samanage.com)
	/// </summary>
	[JsonPropertyName("BaseUrl")]
	public string BaseUrl { get; set; } = string.Empty;

	/// <summary>
	/// The bearer access token for the Service Desk API
	/// </summary>
	[JsonPropertyName("AccessToken")]
	public string AccessToken { get; set; } = string.Empty;
}
