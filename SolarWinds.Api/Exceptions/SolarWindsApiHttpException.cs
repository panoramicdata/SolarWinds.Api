using System;
using System.Net;
using System.Net.Http;

namespace SolarWinds.Api.Exceptions;

public class SolarWindsApiHttpException : Exception
{
	public HttpStatusCode StatusCode { get; }
	public string Content { get; }

	public SolarWindsApiHttpException(HttpResponseMessage httpResponseMessage) : base(httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult())
	{
		StatusCode = httpResponseMessage.StatusCode;
		Content = httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
	}

	public SolarWindsApiHttpException() : this("An HTTP error occurred.")
	{
	}

	public SolarWindsApiHttpException(string message) : base(message)
		=> Content = string.Empty;

	public SolarWindsApiHttpException(string message, Exception innerException) : base(message, innerException)
		=> Content = string.Empty;
}
