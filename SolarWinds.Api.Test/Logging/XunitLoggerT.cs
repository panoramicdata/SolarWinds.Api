using Microsoft.Extensions.Logging;
using System;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test.Logging
{
	public class XunitLogger<T> : ILogger<T>, IDisposable
	{
		private readonly ITestOutputHelper _output;

		public XunitLogger(ITestOutputHelper output) => _output = output;

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) => _output.WriteLine(state.ToString());

		public bool IsEnabled(LogLevel logLevel) => true;

		public IDisposable BeginScope<TState>(TState state) => this;

		public void Dispose()
		{
		}
	}
}