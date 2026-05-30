using Microsoft.Extensions.Logging;

namespace SolarWinds.Api.Test.Logging;

public class XunitLogger<T>(ITestOutputHelper output) : ILogger<T>, IDisposable
{
	private readonly ITestOutputHelper _output = output;

	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
		=> _output.WriteLine(formatter(state, exception) ?? state?.ToString() ?? string.Empty);

	public bool IsEnabled(LogLevel logLevel) => true;

	public IDisposable BeginScope<TState>(TState state) where TState : notnull => this;

	public void Dispose()
	{
	}
}