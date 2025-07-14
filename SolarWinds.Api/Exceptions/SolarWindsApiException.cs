using System;

namespace SolarWinds.Api.Exceptions;

public class SolarWindsApiDeserializationException : Exception
{
	public SolarWindsApiDeserializationException()
	{
	}

	public SolarWindsApiDeserializationException(string message) : base(message)
	{
	}

	public SolarWindsApiDeserializationException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
