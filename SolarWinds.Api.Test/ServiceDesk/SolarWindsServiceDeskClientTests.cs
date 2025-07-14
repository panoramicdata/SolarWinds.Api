using System;
using AwesomeAssertions;
using Xunit;

namespace SolarWinds.Api.Test.ServiceDesk;

public class SolarWindsServiceDeskClientTests
{
	[Fact]
	public void Constructor_WithValidOptions_InitializesClientAndApis()
	{
		// Arrange
		var options = new SolarWindsServiceDeskClientOptions
		{
			BaseUrl = "https://api.samanage.com",
			AccessToken = "test_token"
		};

		// Act
		var client = new SolarWindsServiceDeskClient(options);

		// Assert
		client.Should().NotBeNull();
		client.Tickets.Should().NotBeNull();
		client.Users.Should().NotBeNull();
		client.Incidents.Should().NotBeNull();
		client.OtherAssets.Should().NotBeNull();
	}

	[Theory]
	[InlineData(null, "test_token", "BaseUrl must be provided. (Parameter 'baseUrl')")]
	[InlineData("", "test_token", "BaseUrl must be provided. (Parameter 'baseUrl')")]
	[InlineData("https://api.samanage.com", null, "AccessToken must be provided. (Parameter 'accessToken')")]
	[InlineData("https://api.samanage.com", "", "AccessToken must be provided. (Parameter 'accessToken')")]
	public void Constructor_WithInvalidOptions_ThrowsArgumentException(
		string baseUrl, string accessToken, string expectedErrorMessage)
	{
		// Arrange
		var options = new SolarWindsServiceDeskClientOptions
		{
			BaseUrl = baseUrl,
			AccessToken = accessToken
		};

		// Act & Assert
		((Action)(() => _ = new SolarWindsServiceDeskClient(options))).Should().Throw<ArgumentException>().WithMessage(expectedErrorMessage);
	}

	[Fact]
	public void Constructor_WithNullOptions_ThrowsArgumentNullException()
	{
		// Arrange
		SolarWindsServiceDeskClientOptions options = null;

		// Act & Assert
		((Action)(() => _ = new SolarWindsServiceDeskClient(options))).Should().Throw<ArgumentNullException>();
	}
}