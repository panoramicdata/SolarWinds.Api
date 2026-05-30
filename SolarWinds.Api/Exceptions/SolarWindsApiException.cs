namespace SolarWinds.Api.Exceptions;

/// <summary>
/// Represents an error that occurs while deserializing SolarWinds API payloads.
/// </summary>
public class SolarWindsApiDeserializationException : Exception
{
	/// <summary>
	/// Initializes a new instance of the <see cref="SolarWindsApiDeserializationException"/> class.
	/// </summary>
	public SolarWindsApiDeserializationException()
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="SolarWindsApiDeserializationException"/> class with an error message.
	/// </summary>
	/// <param name="message">Explanation of the deserialization failure.</param>
	public SolarWindsApiDeserializationException(string message) : base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="SolarWindsApiDeserializationException"/> class with an inner exception.
	/// </summary>
	/// <param name="message">Explanation of the deserialization failure.</param>
	/// <param name="innerException">Underlying exception that caused the failure.</param>
	public SolarWindsApiDeserializationException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
