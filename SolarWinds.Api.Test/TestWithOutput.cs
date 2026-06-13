using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SolarWinds.Api.Test.Logging;

namespace SolarWinds.Api.Test;

/// <summary>
/// Base type for integration tests that need shared logging, time windows, and lazy API clients.
/// </summary>
public abstract class TestWithOutput
{
	/// <summary>
	/// Gets the logger instance for the current test.
	/// </summary>
	protected ILogger Logger { get; }

	/// <summary>
	/// Gets a default cancellation token used by test API calls.
	/// </summary>
	protected static CancellationToken CancellationToken => default;

	/// <summary>
	/// Initializes a new instance of the <see cref="TestWithOutput"/> class.
	/// </summary>
	/// <param name="iTestOutputHelper">The xUnit output helper used to emit logs.</param>
	protected TestWithOutput(ITestOutputHelper iTestOutputHelper)
	{
		// Set up logger
		Logger = LoggerFactory.Create(builder => builder
			.SetMinimumLevel(LogLevel.Trace)
			.AddDebug())
			.AddXunit(iTestOutputHelper, LogLevel.Trace)
			.CreateLogger<TestWithOutput>();

		var nowUtc = DateTimeOffset.UtcNow;
		StartEpoch = nowUtc.AddDays(-30).ToUnixTimeSeconds();
		EndEpoch = nowUtc.ToUnixTimeSeconds();
		Stopwatch = Stopwatch.StartNew();

		_orionClientLazy = new Lazy<SolarWindsOrionClient>(CreateOrionClient);
		_serviceDeskClientLazy = new Lazy<SolarWindsServiceDeskClient>(CreateServiceDeskClient);
	}

	private Stopwatch Stopwatch { get; }

	/// <summary>
	/// Gets the start epoch (Unix seconds) used in range-based test queries.
	/// </summary>
	protected long StartEpoch { get; }

	/// <summary>
	/// Gets the end epoch (Unix seconds) used in range-based test queries.
	/// </summary>
	protected long EndEpoch { get; }

	/// <summary>
	/// Only construct this when it's needed
	/// </summary>
	protected SolarWindsOrionClient OrionClient => _orionClientLazy.Value;

	/// <summary>
	/// Asserts that the test execution completed within the specified duration.
	/// </summary>
	/// <param name="durationSeconds">Maximum expected duration in seconds.</param>
	protected void AssertFast(int durationSeconds) => Assert.InRange(Stopwatch.ElapsedMilliseconds, 0, durationSeconds * 1000);

	/// <summary>
	/// Only construct this when it's needed. Reads ServiceDesk credentials from User Secrets
	/// (key: ServiceDesk:BaseUrl, ServiceDesk:AccessToken).
	/// </summary>
	protected SolarWindsServiceDeskClient ServiceDeskClient => _serviceDeskClientLazy.Value;

	private readonly Lazy<SolarWindsOrionClient> _orionClientLazy;

	private readonly Lazy<SolarWindsServiceDeskClient> _serviceDeskClientLazy;

	private SolarWindsOrionClient CreateOrionClient()
	{
		// Deserialize JSON directly from a file
		if (!File.Exists("appsettings.json"))
		{
			throw new FileNotFoundException("Required test configuration file was not found.", "appsettings.json");
		}

		Config? config;
		using (var file = File.OpenText("appsettings.json"))
		{
			var serializer = new Newtonsoft.Json.JsonSerializer();
			config = (Config?)serializer.Deserialize(file, typeof(Config));
		}

		if (config == null)
		{
			throw new InvalidDataException("Failed to deserialize appsettings.json into test configuration.");
		}

		var options = new SolarWindsOrionClientOptions
		{
			Hostname = config.Hostname,
			Username = config.Username,
			Password = config.Password,
			Port = config.Port,
			IgnoreSslCertificateErrors = config.IgnoreSslCertificateErrors,
			Logger = Logger
		};

		return new SolarWindsOrionClient(options);
	}

	private SolarWindsServiceDeskClient CreateServiceDeskClient()
	{
		var configuration = new ConfigurationBuilder()
			.AddUserSecrets<TestWithOutput>()
			.Build();

		var baseUrl = configuration["ServiceDesk:BaseUrl"] ?? string.Empty;
		var accessToken = configuration["ServiceDesk:AccessToken"] ?? string.Empty;

		if (string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(accessToken))
		{
			throw new InvalidOperationException(
				"ServiceDesk credentials not found in User Secrets. " +
				"Run: dotnet user-secrets set \"ServiceDesk:BaseUrl\" \"https://api.samanage.com\" " +
				"and: dotnet user-secrets set \"ServiceDesk:AccessToken\" \"<your-token>\"");
		}

		var options = new SolarWindsServiceDeskClientOptions
		{
			BaseUrl = baseUrl,
			AccessToken = accessToken,
			Logger = Logger
		};

		return new SolarWindsServiceDeskClient(options);
	}
}
