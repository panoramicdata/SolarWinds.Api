using Microsoft.Extensions.Logging;

namespace SolarWinds.Api.Test.Logging;

/// <summary>
/// Represents this type.
/// </summary>
public class XunitLogger<T>(ITestOutputHelper output) : ILogger<T>, IDisposable
{
	private readonly ITestOutputHelper _output = output;

	/// <summary>
	/// Executes Log.
	/// </summary>
	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
		=> _output.WriteLine(formatter(state, exception) ?? state?.ToString() ?? string.Empty);

	/// <summary>
	/// Executes IsEnabled.
	/// </summary>
	public bool IsEnabled(LogLevel logLevel) => true;

	/// <summary>
	/// Executes BeginScope.
	/// </summary>
	public IDisposable BeginScope<TState>(TState state) where TState : notnull => this;

	/// <summary>
	/// Executes Dispose.
	/// </summary>
	public void Dispose()
	{
	}
}
