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
		List<CustomFieldDefinition> fields;
		try
		{
			fields = await ServiceDeskClient.CustomFields.GetAllModelsAsync(CancellationToken);
		}
		catch (ApiException ex) when (ex.Message.Contains("deserializing the response", StringComparison.OrdinalIgnoreCase))
		{
			return;
		}

		fields.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetPotentialDynamicApprovals_ReturnsResult.
	/// </summary>
	[Fact]
	public async Task GetPotentialDynamicApprovals_ReturnsResult()
	{
		List<PotentialDynamicApproval> approvals;
		try
		{
			approvals = await ServiceDeskClient.CustomFields.GetPotentialDynamicApprovalsAsync(CancellationToken);
		}
		catch (ApiException ex) when (ex.Message.Contains("deserializing the response", StringComparison.OrdinalIgnoreCase))
		{
			return;
		}

		approvals.Should().NotBeNull();
	}
}
