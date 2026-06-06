using Microsoft.Extensions.Configuration;

namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class ItsmStatesIntegrationTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Executes IncidentTypes_TypesListPortalShape_ReturnsResult.
	/// </summary>
	[Fact]
	public async Task IncidentTypes_TypesListPortalShape_ReturnsResult()
	{
		if (!ShouldRunIntegrationTest())
		{
			return;
		}

		var result = await ServiceDeskClient.IncidentTypes.GetTypesListAsync(new GetIncidentTypesRequest
		{
			Page = 1,
			Model = "Incident",
			OpType = "update",
			PerPage = 25,
			Name = string.Empty,
			IsPortalMode = false,
		}, CancellationToken);

		result.Should().NotBeNull();
	}

	/// <summary>
	/// Executes SetupItsmStates_InitTabsDataPortalShape_ReturnsResult.
	/// </summary>
	[Fact]
	public async Task SetupItsmStates_InitTabsDataPortalShape_ReturnsResult()
	{
		if (!ShouldRunIntegrationTest())
		{
			return;
		}

		var result = await ServiceDeskClient.SetupItsmStates.InitTabsDataAsync(new GetItsmStatesInitTabsDataRequest
		{
			CustomStatesIncidentVisibilityKey = "feature_acquired",
			CustomStatesIncidentVisibilityValue = "custom_incident_lifecycle",
			CustomStatesChangeVisibilityKey = "feature_acquired",
			CustomStatesChangeVisibilityValue = "custom_incident_lifecycle",
			CustomStatesProjectVisibilityValue = "Hiddables::EnableProjectManagementFeature",
			CustomStatesAssetVisibilityKey = "feature_enabled",
			CustomStatesAssetVisibilityValue = "Hiddables::AssetsCustomStates",
			IsPortalMode = false,
		}, CancellationToken);

		result.Should().NotBeNull();
	}

	/// <summary>
	/// Executes SetupItsmStates_InitStatesDataPortalShape_ReturnsResult.
	/// </summary>
	[Fact]
	public async Task SetupItsmStates_InitStatesDataPortalShape_ReturnsResult()
	{
		if (!ShouldRunIntegrationTest())
		{
			return;
		}

		var result = await ServiceDeskClient.SetupItsmStates.InitStatesDataAsync(new GetItsmStatesInitStatesDataRequest
		{
			ItsmType = "Incident",
			IsPortalMode = false,
		}, CancellationToken);

		result.Should().NotBeNull();
		(result.States.Count > 0 || result.ExtensionData?.Count > 0).Should().BeTrue();
	}

	private static bool ShouldRunIntegrationTest()
	{
		var configuration = new ConfigurationBuilder()
			.AddUserSecrets<ItsmStatesIntegrationTests>()
			.Build();

		return bool.TryParse(configuration["ServiceDesk:Lifecycle:RunTests"], out var shouldRun)
			&& shouldRun;
	}
}
