using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SolarWinds.Api.ServiceDesk;
using SolarWinds.Api.Test.Logging;
using Xunit;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test;

public abstract class TestWithOutput
{
	private SolarWindsClient? _client;
	private SolarWindsServiceDeskClient? _serviceDeskClient;

	protected ILogger Logger { get; }

	protected static CancellationToken CancellationToken => default;

	protected TestWithOutput(ITestOutputHelper iTestOutputHelper)
	{
		// Set up logger
		Logger = LoggerFactory.Create(builder => builder
			.SetMinimumLevel(LogLevel.Trace)
			.AddDebug())
			.AddXunit(iTestOutputHelper, LogLevel.Trace)
			.CreateLogger<SolarWindsClient>();

		var nowUtc = DateTimeOffset.UtcNow;
		StartEpoch = nowUtc.AddDays(-30).ToUnixTimeSeconds();
		EndEpoch = nowUtc.ToUnixTimeSeconds();
		Stopwatch = Stopwatch.StartNew();
	}

	private Stopwatch Stopwatch { get; }

	protected long StartEpoch { get; }
	protected long EndEpoch { get; }

	/// <summary>
	/// Only construct this when it's needed
	/// </summary>
	protected SolarWindsClient Client
	{
		get
		{
			// Do we have one already?
			if (_client != null)
			{
				// Yes - return that.
				return _client;
			}
			// No - create one
			// Deserialize JSON directly from a file
			if (!File.Exists("appsettings.json"))
			{
				throw new FileNotFoundException("Required test configuration file was not found.", "appsettings.json");
			}
			Config? config;
			using (var file = File.OpenText("appsettings.json"))
			{
				var serializer = new JsonSerializer();
				config = (Config?)serializer.Deserialize(file, typeof(Config));
			}

			if (config == null)
			{
				throw new InvalidDataException("Failed to deserialize appsettings.json into test configuration.");
			}

			return _client = new SolarWindsClient(config.Hostname, config.Username, config.Password, config.Port, config.IgnoreSslCertificateErrors);
		}
	}

	protected void AssertFast(int durationSeconds) => Assert.InRange(Stopwatch.ElapsedMilliseconds, 0, durationSeconds * 1000);

	/// <summary>
	/// Only construct this when it's needed. Reads ServiceDesk credentials from User Secrets
	/// (key: ServiceDesk:BaseUrl, ServiceDesk:AccessToken).
	/// </summary>
	protected SolarWindsServiceDeskClient ServiceDeskClient
	{
		get
		{
			if (_serviceDeskClient != null)
			{
				return _serviceDeskClient;
			}

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

			return _serviceDeskClient = new SolarWindsServiceDeskClient(new SolarWindsServiceDeskClientOptions
			{
				BaseUrl = baseUrl,
				AccessToken = accessToken,
				Logger = Logger,
			});
		}
	}
}