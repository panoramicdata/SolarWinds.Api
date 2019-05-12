using System;
using System.Net;
using System.Net.Http;

namespace SolarWinds.Api.Exceptions
{
	public class SolarWindsApiHttpException : Exception
	{
		public HttpStatusCode StatusCode { get; }
		public string Content { get; }

		public SolarWindsApiHttpException(HttpResponseMessage httpResponseMessage) : base(httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult())
		{
			StatusCode = httpResponseMessage.StatusCode;
			Content = httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public SolarWindsApiHttpException()
		{
		}

		protected SolarWindsApiHttpException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
		}

		public SolarWindsApiHttpException(string message) : base(message)
		{
		}

		public SolarWindsApiHttpException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
