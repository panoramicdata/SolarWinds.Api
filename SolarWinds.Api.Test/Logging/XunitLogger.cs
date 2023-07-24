using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test.Logging
{
	public class XunitLogger : ILogger
	{
		private static readonly string[] NewLineChars = { Environment.NewLine };
		private readonly string _category;
		private readonly LogLevel _minLogLevel;
		private readonly ITestOutputHelper _output;

		public XunitLogger(ITestOutputHelper output, string category, LogLevel minLogLevel)
		{
			_minLogLevel = minLogLevel;
			_category = category;
			_output = output;
		}

		public void Log<TState>(
			LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (!IsEnabled(logLevel))
			{
				return;
			}

			// Buffer the message into a single string in order to avoid shearing the message when running across multiple threads.
			var messageBuilder = new StringBuilder();

			var firstLinePrefix = $"| {_category} {logLevel}: ";
			var lines = formatter(state, exception).Split(NewLineChars, StringSplitOptions.RemoveEmptyEntries);
			messageBuilder
				.Append(firstLinePrefix)
				.AppendLine(lines.FirstOrDefault());

			var additionalLinePrefix = "|" + new string(' ', firstLinePrefix.Length - 1);
			foreach (var line in lines.Skip(1))
			{
				messageBuilder
					.Append(additionalLinePrefix)
					.AppendLine(line);
			}

			if (exception != null)
			{
				lines = exception.ToString().Split(NewLineChars, StringSplitOptions.RemoveEmptyEntries);
				additionalLinePrefix = "| ";
				foreach (var line in lines.Skip(1))
				{
					messageBuilder
						.Append(additionalLinePrefix)
						.AppendLine(line);
				}
			}

			// Remove the last line-break, because ITestOutputHelper only has WriteLine.
			var message = messageBuilder.ToString();
			if (message.EndsWith(Environment.NewLine))
			{
				message = message.Substring(0, message.Length - Environment.NewLine.Length);
			}

			try
			{
				_output.WriteLine(message);
			}
			catch (Exception)
			{
				// TODO: Should we do something here?
			}
		}

		public bool IsEnabled(LogLevel logLevel)
			=> logLevel >= _minLogLevel;

		public IDisposable BeginScope<TState>(TState state)
			=> new NullScope();

		private class NullScope : IDisposable
		{
			public void Dispose()
			{
			}
		}
	}
}