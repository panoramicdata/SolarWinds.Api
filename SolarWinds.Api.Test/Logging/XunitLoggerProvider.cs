using Microsoft.Extensions.Logging;

namespace SolarWinds.Api.Test.Logging;

/// <summary>
/// Represents this type.
/// </summary>
public class XunitLoggerProvider(ITestOutputHelper output, LogLevel minLevel = LogLevel.Trace) : ILoggerProvider
{
	private readonly ITestOutputHelper _output = output;
	private readonly LogLevel _minLevel = minLevel;

	/// <summary>
	/// Executes CreateLogger.
	/// </summary>
	public ILogger CreateLogger(string categoryName) => new XunitLogger(_output, categoryName, _minLevel);

	/// <summary>
	/// Executes Dispose.
	/// </summary>
	public void Dispose()
	{
		// No resources to dispose; required by IDisposable interface
	}
}
