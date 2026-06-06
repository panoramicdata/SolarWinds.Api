namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class AuditTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Executes GetAll_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_ReturnsItems()
	{
		List<Audit>? items = null;
		ApiException? lastServerException = null;

		for (var attempt = 1; attempt <= 3; attempt++)
		{
			try
			{
				items = await ServiceDeskClient.Audits.GetAsync(CancellationToken);
				break;
			}
			catch (ApiException ex) when ((int)ex.StatusCode >= 500)
			{
				lastServerException = ex;
				if (attempt < 3)
				{
					await Task.Delay(TimeSpan.FromSeconds(1), CancellationToken);
				}
			}
		}

		if (items is null && lastServerException is not null)
		{
			throw lastServerException;
		}

		items.Should().NotBeNull();
		items.Should().NotBeEmpty();
	}
}
