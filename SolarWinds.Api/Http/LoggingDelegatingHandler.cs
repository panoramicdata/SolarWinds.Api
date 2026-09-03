using System.Text;
using Microsoft.Extensions.Logging;

namespace SolarWinds.Api.Http;

/// <summary>
/// An <see cref="DelegatingHandler"/> that logs HTTP request and response details.
/// </summary>
public class LoggingDelegatingHandler(ILogger logger) : DelegatingHandler
{
	/// <summary>
	/// Sends an HTTP request and logs request/response metadata and bodies at debug level.
	/// </summary>
	/// <param name="request">HTTP request to send.</param>
	/// <param name="cancellationToken">Token used to cancel the operation.</param>
	/// <returns>The HTTP response from the inner handler.</returns>
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var sb = new StringBuilder();
		sb.AppendLine($">>> {request.Method} {request.RequestUri}");
		sb.AppendRedacted(request.Headers);

		if (request.Content != null)
		{
			sb.AppendRedacted(request.Content.Headers);

			var body = await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
			if (!string.IsNullOrWhiteSpace(body))
			{
				sb.AppendLine($"  Body: {body}");
			}
		}

		logger.LogDebug("{RequestLog}", sb.ToString());

		var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

		var rsb = new StringBuilder();
		rsb.AppendLine($"<<< {(int)response.StatusCode} {response.ReasonPhrase} ({request.Method} {request.RequestUri})");
		rsb.AppendRedacted(response.Headers);

		if (response.Content != null)
		{
			rsb.AppendRedacted(response.Content.Headers);

			var responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
			if (!string.IsNullOrWhiteSpace(responseBody))
			{
				rsb.AppendLine($"  Body: {responseBody}");
			}
		}

		logger.LogDebug("{ResponseLog}", rsb.ToString());

		return response;
	}
}
