using System;
using System.Net;
using System.Net.Http;

namespace SolarWinds.Api.Exceptions;

public class SolarWindsApiHttpException : Exception
{
	public HttpStatusCode StatusCode { get; }
	public string Content { get; }

	public SolarWindsApiHttpException(HttpResponseMessage httpResponseMessage)
		: base(CreateMessage(httpResponseMessage, out var statusCode, out var content))
	{
		StatusCode = statusCode;
		Content = content;
	}

	private static string CreateMessage(HttpResponseMessage httpResponseMessage, out HttpStatusCode statusCode, out string content)
	{
		statusCode = httpResponseMessage.StatusCode;
		content = httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();

		var reasonPhrase = string.IsNullOrWhiteSpace(httpResponseMessage.ReasonPhrase)
			? statusCode.ToString()
			: httpResponseMessage.ReasonPhrase;

		return string.IsNullOrWhiteSpace(content)
			? $"SolarWinds HTTP Exception: {(int)statusCode} {reasonPhrase}"
			: $"SolarWinds HTTP Exception: {(int)statusCode} {reasonPhrase}. Response: {content}";
	}

	public SolarWindsApiHttpException() : this("An HTTP error occurred.")
	{
	}

	public SolarWindsApiHttpException(string message) : base(message)
		=> Content = string.Empty;

	public SolarWindsApiHttpException(string message, Exception innerException) : base(message, innerException)
		=> Content = string.Empty;
}
