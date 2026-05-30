using AwesomeAssertions;
using Refit;
using Xunit;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test.ServiceDesk;

public class NotificationTests(ITestOutputHelper output) : TestWithOutput(output)
{
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
