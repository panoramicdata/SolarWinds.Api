namespace SolarWinds.Api.Http;

/// <summary>
/// Refuses, when the client is read-only, any request other than a SWQL query before it is sent.
/// </summary>
internal sealed class SolarWindsOrionHttpClientHandler(SolarWindsOrionClientOptions options) : HttpClientHandler
{
	// The SWIS Query endpoint takes the SWQL in a POST body but only ever reads, so it is allowed by path.
	private const string QueryPathSuffix = "/Json/Query";

	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		if (options.IsReadOnly && ChangesServer(request))
		{
			throw new InvalidOperationException($"The client is read-only; {request.Method} {request.RequestUri?.AbsolutePath} would change the server.");
		}

		return base.SendAsync(request, cancellationToken);
	}

	private static bool ChangesServer(HttpRequestMessage request)
	{
		if (request.Method == HttpMethod.Get || request.Method == HttpMethod.Head || request.Method == HttpMethod.Options)
		{
			return false;
		}

		var path = request.RequestUri?.AbsolutePath.TrimEnd('/') ?? string.Empty;

		return request.Method != HttpMethod.Post || !path.EndsWith(QueryPathSuffix, StringComparison.OrdinalIgnoreCase);
	}
}
