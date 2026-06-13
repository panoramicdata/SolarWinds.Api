using Microsoft.Extensions.Logging.Abstractions;

namespace SolarWinds.Api.Test;

/// <summary>
/// Represents this type.
/// </summary>
public class SolarWindsOrionClientConstructorTests(ITestOutputHelper iTestOutputHelper) : TestWithOutput(iTestOutputHelper)
{
	/// <summary>
	/// A valid options object should result in a successful construction
	/// </summary>
	[Fact]
	public void ValidOptions_Succeeds()
	{
		var options = new SolarWindsOrionClientOptions
		{
			Hostname = "hostname",
			Username = "username",
			Password = "password",
			Logger = NullLogger.Instance
		};
		var client = new SolarWindsOrionClient(options);
		client.Should().NotBeNull();
	}
}
