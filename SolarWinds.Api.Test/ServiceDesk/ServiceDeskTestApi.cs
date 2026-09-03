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
	/// <remarks>
	/// Service Desk takes a multi-valued filter as a repeated key, Rails-style:
	/// <c>department[]=1&amp;department[]=2</c>. This overload keeps only the last value for such a
	/// key; use <see cref="ParseQueryValues"/> to assert on all of them.
	/// </remarks>
	/// <param name="uri">The captured request URI.</param>
	public static Dictionary<string, string> ParseQuery(Uri uri)
	{
		var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		foreach (var (key, values) in ParseQueryValues(uri))
		{
			result[key] = values[^1];
		}

		return result;
	}

	/// <summary>
	/// Splits a captured request's query string into every value supplied for each name, in the
	/// order they appear. Use this for the repeated-key filters, where a single-valued view would
	/// silently drop all but the last value.
	/// </summary>
	/// <param name="uri">The captured request URI.</param>
	public static Dictionary<string, string[]> ParseQueryValues(Uri uri)
	{
		var result = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
		var query = uri.Query;
		if (string.IsNullOrWhiteSpace(query))
		{
			return [];
		}

		foreach (var pair in query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries))
		{
			var pieces = pair.Split('=', 2);
			var key = Uri.UnescapeDataString(pieces[0]);
			var value = pieces.Length > 1 ? Uri.UnescapeDataString(pieces[1]) : string.Empty;

			if (!result.TryGetValue(key, out var values))
			{
				values = [];
				result[key] = values;
			}

			values.Add(value);
		}

		return result.ToDictionary(entry => entry.Key, entry => entry.Value.ToArray(), StringComparer.OrdinalIgnoreCase);
	}
}
