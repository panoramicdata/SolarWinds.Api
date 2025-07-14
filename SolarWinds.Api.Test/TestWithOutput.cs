using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SolarWinds.Api.Test.Logging;
using System;
using System.Diagnostics;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using AwesomeAssertions;

namespace SolarWinds.Api.Test;

public abstract class TestWithOutput
{
	private SolarWindsClient _client;

	protected ILogger Logger { get; }

	protected TestWithOutput(ITestOutputHelper iTestOutputHelper)
	{
		// Set up logger
		Logger = LoggerFactory.Create(builder => builder.AddDebug())
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
			Config config;
			using (var file = File.OpenText("appsettings.json"))
			{
				var serializer = new JsonSerializer();
				config = (Config)serializer.Deserialize(file, typeof(Config));
			}

			return _client = new SolarWindsClient(config.Hostname, config.Username, config.Password, config.Port, config.IgnoreSslCertificateErrors);
		}
	}

	protected void AssertFast(int durationSeconds) => Assert.InRange(Stopwatch.ElapsedMilliseconds, 0, durationSeconds * 1000);
}