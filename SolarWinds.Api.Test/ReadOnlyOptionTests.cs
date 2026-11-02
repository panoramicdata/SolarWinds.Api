using Microsoft.Extensions.Logging.Abstractions;
using SolarWinds.Api.Http;
using SolarWinds.Api.Test.ServiceDesk;

namespace SolarWinds.Api.Test;

/// <summary>
/// <c>IsReadOnly</c> on both client options needs no tenant, credentials or network.
/// </summary>
public class ReadOnlyOptionTests
{
	private static readonly Uri _unreachable = new("http://127.0.0.1:1/");

	private static SolarWindsServiceDeskClientOptions ServiceDeskOptions(bool isReadOnly) => new()
	{
		BaseUrl = _unreachable.AbsoluteUri,
		AccessToken = "not-a-real-token",
		IsReadOnly = isReadOnly
	};

	private static SolarWindsOrionClientOptions OrionOptions(bool isReadOnly) => new()
	{
		Hostname = "127.0.0.1",
		Username = "u",
		Password = "p",
		Logger = NullLogger.Instance,
		IsReadOnly = isReadOnly
	};

	/// <summary>The option is off unless asked for.</summary>
	[Fact]
	public void IsReadOnly_DefaultsToFalse()
	{
		ServiceDeskOptions(false).IsReadOnly.Should().BeFalse();
		new SolarWindsServiceDeskClientOptions().IsReadOnly.Should().BeFalse();
		OrionOptions(false).IsReadOnly.Should().BeFalse();
	}

	/// <summary>A Service Desk write is refused before any connection is made (nothing listens on the address).</summary>
	[Fact]
	public async Task ServiceDesk_ReadOnly_CreateIncident_IsRefusedBeforeSending()
	{
		var client = new SolarWindsServiceDeskClient(ServiceDeskOptions(isReadOnly: true));

		var act = () => client.Incidents.CreateAsync(new IncidentCreateRequest(), TestContext.Current.CancellationToken);

		var thrown = await act.Should().ThrowAsync<Exception>();
		(thrown.Which is InvalidOperationException || thrown.Which.InnerException is InvalidOperationException).Should().BeTrue();
		thrown.Which.ToString().Should().Contain("read-only");
	}

	/// <summary>Every non-read method is refused and nothing reaches the next handler.</summary>
	[Theory]
	[InlineData("POST")]
	[InlineData("PUT")]
	[InlineData("DELETE")]
	[InlineData("PATCH")]
	public async Task ServiceDesk_ReadOnly_NonReadMethods_NeverReachTheTransport(string method)
	{
		var capture = new CaptureHandler();
		using var invoker = new HttpMessageInvoker(new SolarWindsServiceDeskBackingOffHandler(ServiceDeskOptions(true)) { InnerHandler = capture });

		var act = () => invoker.SendAsync(new HttpRequestMessage(new HttpMethod(method), "http://desk.test/incidents.json"), TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<InvalidOperationException>();
		capture.LastRequest.Should().BeNull();
	}

	/// <summary>A Service Desk read still goes through.</summary>
	[Fact]
	public async Task ServiceDesk_ReadOnly_Get_StillGoesThrough()
	{
		var capture = new CaptureHandler();
		using var invoker = new HttpMessageInvoker(new SolarWindsServiceDeskBackingOffHandler(ServiceDeskOptions(true)) { InnerHandler = capture });

		using var response = await invoker.SendAsync(new HttpRequestMessage(HttpMethod.Get, "http://desk.test/incidents.json"), TestContext.Current.CancellationToken);

		response.StatusCode.Should().Be(HttpStatusCode.OK);
		capture.LastRequest.Should().NotBeNull();
	}

	/// <summary>With the option off the Service Desk write is sent.</summary>
	[Fact]
	public async Task ServiceDesk_NotReadOnly_Post_IsSent()
	{
		var capture = new CaptureHandler();
		using var invoker = new HttpMessageInvoker(new SolarWindsServiceDeskBackingOffHandler(ServiceDeskOptions(false)) { InnerHandler = capture });

		using var response = await invoker.SendAsync(new HttpRequestMessage(HttpMethod.Post, "http://desk.test/incidents.json"), TestContext.Current.CancellationToken);

		capture.LastRequest.Should().NotBeNull();
	}

	/// <summary>Orion refuses anything but a SWQL query, before sending.</summary>
	[Fact]
	public async Task Orion_ReadOnly_NonQueryRequest_IsRefusedBeforeSending()
	{
		using var invoker = new HttpMessageInvoker(new SolarWindsOrionHttpClientHandler(OrionOptions(isReadOnly: true)));

		var act = () => invoker.SendAsync(new HttpRequestMessage(HttpMethod.Post, "https://127.0.0.1:1/SolarWinds/InformationService/v3/Json/Create/Orion.Nodes"), TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<InvalidOperationException>();
	}

	/// <summary>The SWQL query POST is allowed by path: it fails on the missing server, not on the policy.</summary>
	[Fact]
	public async Task Orion_ReadOnly_QueryRequest_IsNotRefused()
	{
		using var invoker = new HttpMessageInvoker(new SolarWindsOrionHttpClientHandler(OrionOptions(isReadOnly: true)));

		var act = () => invoker.SendAsync(new HttpRequestMessage(HttpMethod.Post, "https://127.0.0.1:1/SolarWinds/InformationService/v3/Json/Query"), TestContext.Current.CancellationToken);

		var thrown = await act.Should().ThrowAsync<Exception>();
		thrown.Which.Should().BeOfType<HttpRequestException>();
	}
}
