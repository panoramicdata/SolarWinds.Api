namespace SolarWinds.Api.Http;

public partial class SolarWindsServiceDeskBackingOffHandler
{
	private sealed class BufferedRequest
	{
		public BufferedRequest(HttpMethod method, Uri? requestUri, Version version, HttpVersionPolicy versionPolicy)
		{
			Method = method;
			RequestUri = requestUri;
			Version = version;
			VersionPolicy = versionPolicy;
		}

		public HttpMethod Method { get; }
		public Uri? RequestUri { get; }
		public Version Version { get; }
		public HttpVersionPolicy VersionPolicy { get; }
		public byte[]? ContentBytes { get; private set; }
		public string? ContentMediaType { get; private set; }
		public HeaderBag RequestHeaders { get; } = [];
		public HeaderBag ContentHeaders { get; } = [];
		public OptionBag Options { get; } = [];

		public static async Task<BufferedRequest> CreateAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var bufferedRequest = new BufferedRequest(request.Method, request.RequestUri, request.Version, request.VersionPolicy);

			foreach (var header in request.Headers)
			{
				bufferedRequest.RequestHeaders.Add((header.Key, [.. header.Value]));
			}

			foreach (var option in request.Options)
			{
				bufferedRequest.Options.Add((option.Key, option.Value));
			}

			if (request.Content != null)
			{
				bufferedRequest.ContentBytes = await request.Content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
				bufferedRequest.ContentMediaType = request.Content.Headers.ContentType?.MediaType;

				foreach (var header in request.Content.Headers)
				{
					bufferedRequest.ContentHeaders.Add((header.Key, [.. header.Value]));
				}
			}

			return bufferedRequest;
		}

		public HttpRequestMessage CreateRequestMessage()
		{
			var request = new HttpRequestMessage(Method, RequestUri)
			{
				Version = Version,
				VersionPolicy = VersionPolicy,
			};

			foreach (var (Name, Values) in RequestHeaders)
			{
				request.Headers.TryAddWithoutValidation(Name, Values);
			}

			foreach (var (Key, Value) in Options)
			{
				request.Options.Set(new HttpRequestOptionsKey<object?>(Key), Value);
			}

			if (ContentBytes != null)
			{
				request.Content = new ByteArrayContent(ContentBytes);

				foreach (var (Name, Values) in ContentHeaders)
				{
					request.Content.Headers.TryAddWithoutValidation(Name, Values);
				}
			}

			return request;
		}
	}

	private sealed class HeaderBag : List<(string Name, string[] Values)>;
	private sealed class OptionBag : List<(string Key, object? Value)>;
}
