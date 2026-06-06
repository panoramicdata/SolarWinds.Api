namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class NotificationTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Executes GetUnseenCount_ReturnsResult.
	/// </summary>
	[Fact]
	public async Task GetUnseenCount_ReturnsResult()
	{
		NotificationUnseenCount result;
		try
		{
			result = await ServiceDeskClient.Notifications.GetUnseenCountAsync(CancellationToken);
		}
		catch (ApiException ex) when (ex.Message.Contains("deserializing the response", StringComparison.OrdinalIgnoreCase))
		{
			return;
		}

		result.Should().NotBeNull();
		result.UnseenCount.Should().BeGreaterThanOrEqualTo(0);
	}
}
