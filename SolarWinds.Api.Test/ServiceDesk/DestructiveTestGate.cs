using Microsoft.Extensions.Configuration;

namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Decides whether the destructive Service Desk integration tests may run against the configured tenant.
/// </summary>
internal static class DestructiveTestGate
{
	/// <summary>
	/// Builds the user-secrets configuration the integration tests read.
	/// </summary>
	public static IConfiguration Configuration() => new ConfigurationBuilder()
		.AddUserSecrets<TestWithOutput>()
		.Build();

	/// <summary>
	/// Returns <see langword="true"/> only when a destructive run is enabled and the tenant is the dedicated test tenant.
	/// </summary>
	public static bool ShouldRun()
	{
		var configuration = Configuration();

		var runCoverage = bool.TryParse(configuration["ServiceDesk:Coverage:RunDestructiveTests"], out var explicitRun) && explicitRun;
		var runLifecycle = bool.TryParse(configuration["ServiceDesk:Lifecycle:RunTests"], out var lifecycleRun) && lifecycleRun;
		if (!runCoverage && !runLifecycle)
		{
			return false;
		}

		var baseUrl = configuration["ServiceDesk:BaseUrl"] ?? string.Empty;
		return baseUrl.Contains("panoramicdatalimited.samanage.com", StringComparison.OrdinalIgnoreCase);
	}
}
