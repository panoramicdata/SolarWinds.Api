using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolarWinds.Api.Exceptions;
using SolarWinds.Api.Queries;

namespace SolarWinds.Api;

/// <summary>
/// Client for querying the SolarWinds Information Service (SWIS) JSON endpoint.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SolarWindsClient"/> class.
/// </remarks>
/// <param name="hostname">SolarWinds host name.</param>
/// <param name="username">Account user name.</param>
/// <param name="password">Account password.</param>
/// <param name="port">HTTPS port for the SWIS endpoint.</param>
/// <param name="ignoreSslCertificateErrors">Whether to ignore TLS certificate validation errors.</param>
[Obsolete("Use SolarWindsOrionClient instead.", true)]
public class SolarWindsClient(
	string hostname,
	string username,
	string password,
	int port,
	bool ignoreSslCertificateErrors) : SolarWindsClientBase(
		hostname,
		port,
		BuildHandler(username, password, ignoreSslCertificateErrors),
		NullLogger.Instance)
{
	private static HttpClientHandler BuildHandler(string username, string password, bool ignoreSslCertificateErrors)
	{
		var handler = new HttpClientHandler
		{
			Credentials = new NetworkCredential(username, password),
		};
		if (ignoreSslCertificateErrors)
		{
			handler.ServerCertificateCustomValidationCallback = RelaxedCertificateCheck;
		}

		return handler;
	}
}
