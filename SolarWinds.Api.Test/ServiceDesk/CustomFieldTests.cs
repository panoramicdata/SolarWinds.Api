namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class CustomFieldTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Executes GetAllModels_ReturnsResult.
	/// </summary>
	[Fact]
	public async Task GetAllModels_ReturnsResult()
	{
		var fields = await ServiceDeskClient
			.CustomFields
			.GetAllModelsAsync(CancellationToken);

		fields.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetPotentialDynamicApprovals_ReturnsResult.
	/// </summary>
	[Fact]
	public async Task GetPotentialDynamicApprovals_ReturnsResult()
	{
		var approvals = await ServiceDeskClient
				.CustomFields
				.GetPotentialDynamicApprovalsAsync(CancellationToken);

		approvals.Should().NotBeNull();
		approvals.CustomFields.Should().NotBeNull();
	}
}
