using Microsoft.Extensions.Configuration;

namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public partial class ServiceDeskEndpointCoverageTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Deletes a record on a best-effort basis, if it was created at all.
	/// </summary>
	/// <param name="id">The record's id, or <see langword="null"/> if it was never created.</param>
	/// <param name="delete">The delete call to attempt.</param>
	private static async Task DeleteIfCreatedAsync(int? id, Func<int, Task> delete)
	{
		if (id is not > 0)
		{
			return;
		}

		await TryCleanupAsync(() => delete(id.Value));
	}

	private static bool ShouldRunDestructiveIntegrationTests()
	{
		var configuration = GetCoverageConfiguration();

		var runCoverage = bool.TryParse(configuration["ServiceDesk:Coverage:RunDestructiveTests"], out var explicitRun) && explicitRun;
		var runLifecycle = bool.TryParse(configuration["ServiceDesk:Lifecycle:RunTests"], out var lifecycleRun) && lifecycleRun;
		if (!runCoverage && !runLifecycle)
		{
			return false;
		}

		var baseUrl = configuration["ServiceDesk:BaseUrl"] ?? string.Empty;
		return baseUrl.Contains("panoramicdatalimited.samanage.com", StringComparison.OrdinalIgnoreCase);
	}

	private static IConfiguration GetCoverageConfiguration() => new ConfigurationBuilder()
			.AddUserSecrets<ServiceDeskEndpointCoverageTests>()
			.Build();

	private static string RequireSecret(IConfiguration configuration, string key, string guidance)
	{
		var value = configuration[key];
		value.Should().NotBeNullOrWhiteSpace($"missing field information: {guidance}");
		return value!;
	}

	private static bool TryGetSecret(IConfiguration configuration, string key, out string value)
	{
		var raw = configuration[key];
		if (string.IsNullOrWhiteSpace(raw))
		{
			value = string.Empty;
			return false;
		}

		value = raw;
		return true;
	}

	private static bool TryGetIntSecret(IConfiguration configuration, string key, out int value)
	{
		value = 0;
		return TryGetSecret(configuration, key, out var raw)
			&& int.TryParse(raw, out value)
			&& value > 0;
	}

	private static int RequireIntSecret(IConfiguration configuration, string key, string guidance)
	{
		var raw = RequireSecret(configuration, key, guidance);
		var parsed = int.TryParse(raw, out var value) ? value : 0;
		parsed.Should().BePositive($"{key} must be a positive integer");
		return parsed;
	}
}
