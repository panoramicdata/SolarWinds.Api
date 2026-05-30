using System.Net;

namespace SolarWinds.Api.Exceptions;

/// <summary>
/// Represents an HTTP-level failure returned by SolarWinds APIs.
/// </summary>
public class SolarWindsApiHttpException : Exception
{
	/// <summary>
	/// Gets the HTTP status code returned by the server.
	/// </summary>
	public HttpStatusCode StatusCode { get; }

	/// <summary>
	/// Gets the raw response content returned by the server.
	/// </summary>
	public string Content { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="SolarWindsApiHttpException"/> class from an HTTP response.
	/// </summary>
	/// <param name="httpResponseMessage">Response that triggered the exception.</param>
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

	/// <summary>
	/// Initializes a new instance of the <see cref="SolarWindsApiHttpException"/> class with a default message.
	/// </summary>
	public SolarWindsApiHttpException() : this("An HTTP error occurred.")
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="SolarWindsApiHttpException"/> class with an error message.
	/// </summary>
	/// <param name="message">Explanation of the HTTP failure.</param>
	public SolarWindsApiHttpException(string message) : base(message)
		=> Content = string.Empty;

	/// <summary>
	/// Initializes a new instance of the <see cref="SolarWindsApiHttpException"/> class with an inner exception.
	/// </summary>
	/// <param name="message">Explanation of the HTTP failure.</param>
	/// <param name="innerException">Underlying exception that caused the failure.</param>
	public SolarWindsApiHttpException(string message, Exception innerException) : base(message, innerException)
		=> Content = string.Empty;
}
