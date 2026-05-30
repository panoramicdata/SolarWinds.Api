using Microsoft.Extensions.Configuration;

namespace SolarWinds.Api.Test.ServiceDesk;

public class CustomFormTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetById_WithValidId_ReturnsForm()
	{
		// Custom forms require a known ID - skip gracefully if none configured
		var configuration = new ConfigurationBuilder()
			.AddUserSecrets<CustomFormTests>()
			.Build();

		var customFormIdRaw = configuration["ServiceDesk:CustomFormId"];
		if (!int.TryParse(customFormIdRaw, out var customFormId) || customFormId <= 0)
		{
			return;
		}

		var form = await ServiceDeskClient.CustomForms.GetAsync(customFormId, CancellationToken);
		form.Should().NotBeNull();
		form.Id.Should().Be(customFormId);
	}
}
