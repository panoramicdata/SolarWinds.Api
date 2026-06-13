using System.Net;
using Microsoft.Extensions.Logging;

namespace SolarWinds.Api;

/// <summary>
/// Client for querying the SolarWinds Information Service (SWIS) JSON endpoint.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="SolarWindsOrionClient"/> class.
/// </remarks>
/// <param name="options">The client options.</param>
public class SolarWindsOrionClient(SolarWindsOrionClientOptions options) : SolarWindsClientBase(
		options.Hostname,
		options.Port,
		BuildHandler(options),
		options.Logger)
{
	private static HttpClientHandler BuildHandler(SolarWindsOrionClientOptions options)
	{
		var handler = new HttpClientHandler
		{
			Credentials = new NetworkCredential(options.Username, options.Password),
		};

		if (options.IgnoreSslCertificateErrors)
		{
			handler.ServerCertificateCustomValidationCallback = RelaxedCertificateCheck;
		}

		if (options.ProxySettings is not null)
		{
			handler.Proxy = new WebProxy(options.ProxySettings.Uri)
			{
				Credentials = options.ProxySettings.Username is not null
					? new NetworkCredential(options.ProxySettings.Username, options.ProxySettings.Password)
					: null
			};
		}

		return handler;
	}
}
