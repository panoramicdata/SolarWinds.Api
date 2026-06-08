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
		var result = await ServiceDeskClient
			.Notifications
			.GetUnseenCountAsync(CancellationToken);

		result.Should().NotBeNull();
		result.UnseenCount.Should().BeGreaterThanOrEqualTo(0);
	}
}
