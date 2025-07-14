using System;

namespace SolarWinds.Api.Exceptions;

public class SolarWindsApiDeserializationException : Exception
{
	public SolarWindsApiDeserializationException()
	{
	}

	protected SolarWindsApiDeserializationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
	{
	}

	public SolarWindsApiDeserializationException(string message) : base(message)
	{
	}

	public SolarWindsApiDeserializationException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
