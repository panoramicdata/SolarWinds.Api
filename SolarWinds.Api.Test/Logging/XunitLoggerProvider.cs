using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test.Logging;

public class XunitLoggerProvider(ITestOutputHelper output, LogLevel minLevel = LogLevel.Trace) : ILoggerProvider
{
	private readonly ITestOutputHelper _output = output;
	private readonly LogLevel _minLevel = minLevel;

	public ILogger CreateLogger(string categoryName) => new XunitLogger(_output, categoryName, _minLevel);

	public void Dispose()
	{
	}
}