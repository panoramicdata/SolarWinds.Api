using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SolarWinds.Api.Http;

/// <summary>
/// An <see cref="DelegatingHandler"/> that logs HTTP request and response details.
/// </summary>
public class LoggingDelegatingHandler(ILogger logger) : DelegatingHandler
{
	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var sb = new StringBuilder();
		sb.AppendLine($">>> {request.Method} {request.RequestUri}");
		foreach (var header in request.Headers)
		{
			sb.AppendLine($"  {header.Key}: {string.Join(", ", header.Value)}");
		}

		if (request.Content != null)
		{
			foreach (var header in request.Content.Headers)
			{
				sb.AppendLine($"  {header.Key}: {string.Join(", ", header.Value)}");
			}

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
		foreach (var header in response.Headers)
		{
			rsb.AppendLine($"  {header.Key}: {string.Join(", ", header.Value)}");
		}

		if (response.Content != null)
		{
			foreach (var header in response.Content.Headers)
			{
				rsb.AppendLine($"  {header.Key}: {string.Join(", ", header.Value)}");
			}

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
