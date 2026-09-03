namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Shared plumbing for the request-shaping tests: a Refit client wired to a
/// <see cref="CaptureHandler"/> rather than the network, configured exactly as
/// <see cref="SolarWindsServiceDeskClient"/> configures its own, so these tests exercise the real
/// serialization and URL-formatting rules.
/// </summary>
internal static class ServiceDeskTestApi
{
	/// <summary>
	/// The base address the captured requests are resolved against. Never contacted.
	/// </summary>
	public static Uri BaseAddress { get; } = new("https://api.samanage.com");

	/// <summary>
	/// The Refit settings the production client uses.
	/// </summary>
	public static RefitSettings Settings { get; } = new(
		new SystemTextJsonContentSerializer(new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
		}))
	{
		UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
	};

	/// <summary>
	/// Creates an <see cref="HttpClient"/> over the given handler. The caller owns it, and should
	/// dispose it with a <see langword="using"/> declaration.
	/// </summary>
	/// <param name="handler">The handler that answers the requests, normally a <see cref="CaptureHandler"/>.</param>
	public static HttpClient CreateHttpClient(HttpMessageHandler handler)
		=> new(handler)
		{
			BaseAddress = BaseAddress
		};

	/// <summary>
	/// Creates a Refit implementation of an API interface over the given client.
	/// </summary>
	/// <typeparam name="T">The Refit API interface to implement.</typeparam>
	/// <param name="client">The client the implementation sends through.</param>
	public static T CreateApi<T>(HttpClient client)
		=> RestService.For<T>(client, Settings);

	/// <summary>
	/// Splits a captured request's query string into its decoded name/value pairs, so a test can
	/// assert on parameters without depending on the order Refit emits them in.
	/// </summary>
	/// <param name="uri">The captured request URI.</param>
	public static Dictionary<string, string> ParseQuery(Uri uri)
	{
		var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		var query = uri.Query;
		if (string.IsNullOrWhiteSpace(query))
		{
			return result;
		}

		foreach (var pair in query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries))
		{
			var pieces = pair.Split('=', 2);
			var key = Uri.UnescapeDataString(pieces[0]);
			var value = pieces.Length > 1 ? Uri.UnescapeDataString(pieces[1]) : string.Empty;
			result[key] = value;
		}

		return result;
	}
}
