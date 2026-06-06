using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SolarWinds.Api.Test.Logging;

/// <summary>
/// Provides extension methods for wiring xUnit-backed loggers into logging factories and builders.
/// </summary>
public static class XunitLoggerFactoryExtensions
{
	/// <summary>
	/// Executes AddXunit.
	/// </summary>
	public static ILoggingBuilder AddXunit(this ILoggingBuilder builder, ITestOutputHelper output)
	{
		builder.Services.AddSingleton<ILoggerProvider>(new XunitLoggerProvider(output));
		return builder;
	}

	/// <summary>
	/// Executes AddXunit.
	/// </summary>
	public static ILoggingBuilder AddXunit(this ILoggingBuilder builder, ITestOutputHelper output, LogLevel minLevel)
	{
		builder.Services.AddSingleton<ILoggerProvider>(new XunitLoggerProvider(output, minLevel));
		return builder;
	}

	/// <summary>
	/// Executes AddXunit.
	/// </summary>
	public static ILoggerFactory AddXunit(this ILoggerFactory loggerFactory, ITestOutputHelper output)
	{
		loggerFactory.AddProvider(new XunitLoggerProvider(output));
		return loggerFactory;
	}

	/// <summary>
	/// Executes AddXunit.
	/// </summary>
	public static ILoggerFactory AddXunit(this ILoggerFactory loggerFactory, ITestOutputHelper output, LogLevel minLevel)
	{
		loggerFactory.AddProvider(new XunitLoggerProvider(output, minLevel));
		return loggerFactory;
	}
}
