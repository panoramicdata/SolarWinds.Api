using System.Runtime.Serialization;

namespace SolarWinds.Api.Test;

/// <summary>
/// Test config
/// </summary>
[DataContract]
public class Config
{
	/// <summary>
	/// The hostname
	/// </summary>
	[DataMember(Name = "Hostname")]
	public string Hostname { get; internal set; } = string.Empty;

	/// <summary>
	/// The username
	/// </summary>
	[DataMember(Name = "Username")]
	public string Username { get; internal set; } = string.Empty;

	/// <summary>
	/// The password
	/// </summary>
	[DataMember(Name = "Password")]
	public string Password { get; internal set; } = string.Empty;

	/// <summary>
	/// The port
	/// </summary>
	[DataMember(Name = "Port")]
	public int Port { get; internal set; }

	/// <summary>
	/// Whether to ignore SSL Certificate Errors
	/// </summary>
	[DataMember(Name = "IgnoreSslCertificateErrors")]
	public bool IgnoreSslCertificateErrors { get; set; }

	/// <summary>
	/// Configuration for the SolarWinds Service Desk API
	/// </summary>
	[DataMember(Name = "ServiceDesk")]
	public ServiceDeskConfig ServiceDesk { get; internal set; } = new();
}

/// <summary>
/// Service Desk API configuration
/// </summary>
[DataContract]
public class ServiceDeskConfig
{
	/// <summary>
	/// The base URL for the Service Desk API (e.g. https://api.samanage.com)
	/// </summary>
	[DataMember(Name = "BaseUrl")]
	public string BaseUrl { get; internal set; } = string.Empty;

	/// <summary>
	/// The bearer access token for the Service Desk API
	/// </summary>
	[DataMember(Name = "AccessToken")]
	public string AccessToken { get; internal set; } = string.Empty;
}
