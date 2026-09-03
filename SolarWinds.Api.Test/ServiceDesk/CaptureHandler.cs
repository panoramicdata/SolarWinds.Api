namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// An <see cref="HttpMessageHandler"/> test double that records the request it was given and
/// replies with a canned JSON body. Request-shaping tests assert against <see cref="LastRequest"/>
/// to check the URL, query string and payload a Refit interface produced, without a live tenant.
/// </summary>
/// <param name="responseContent">The JSON body to reply with. Defaults to an empty JSON array.</param>
internal sealed class CaptureHandler(string responseContent = "[]") : HttpMessageHandler
{
	/// <summary>
	/// Gets the most recent request that reached this handler, or <see langword="null"/> if none has.
	/// </summary>
	public HttpRequestMessage? LastRequest { get; private set; }

	/// <summary>
	/// Gets or sets the JSON body returned for every request. Settable so that a single handler can
	/// serve a sequence of calls that expect different responses.
	/// </summary>
	public string ResponseContent { get; set; } = responseContent;

	/// <inheritdoc />
	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();

		LastRequest = request;

		return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
		{
			Content = new StringContent(ResponseContent, Encoding.UTF8, "application/json")
		});
	}
}
