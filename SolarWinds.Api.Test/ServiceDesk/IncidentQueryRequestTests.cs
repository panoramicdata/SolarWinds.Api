using System.Net;
using System.Text.Json;
using AwesomeAssertions;
using Refit;
using SolarWinds.Api.ServiceDesk.Helpers;
using SolarWinds.Api.ServiceDesk.Interfaces;
using SolarWinds.Api.ServiceDesk.Models;
using Xunit;

namespace SolarWinds.Api.Test.ServiceDesk;

public class IncidentQueryRequestTests
{
	[Fact]
	public async Task GetAll_WithLayoutLongAndUpdatedRange_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = new HttpClient(capture)
		{
			BaseAddress = new Uri("https://api.samanage.com")
		};

		var incidentsApi = RestService.For<IIncidents>(client, new RefitSettings(
			new SystemTextJsonContentSerializer(new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
			}))
		{
			UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
		});

		await incidentsApi.GetAllAsync(new GetIncidentsRequest
		{
			Layout = ResponseLayout.Long,
			Updated = "Select Date Range",
			UpdatedCustomGte = new DateTime(2023, 3, 2),
			UpdatedCustomLte = new DateTime(2023, 3, 9),
			Page = 2,
			PerPage = 100
		}, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.Method.Should().Be(HttpMethod.Get);
		capture.LastRequest.RequestUri.Should().NotBeNull();
		capture.LastRequest.RequestUri!.AbsolutePath.Should().Be("/incidents.json");

		var query = ParseQuery(capture.LastRequest.RequestUri);
		query.Should().ContainKey("layout");
		query["layout"].Should().Be("long");
		query.Should().ContainKey("updated");
		query["updated"].Should().Be("Select Date Range");
		query.Should().ContainKey("updated_custom_gte");
		query["updated_custom_gte"].Should().Be("Mar 2 2023");
		query.Should().ContainKey("updated_custom_lte");
		query["updated_custom_lte"].Should().Be("Mar 9 2023");
		query.Should().ContainKey("page");
		query["page"].Should().Be("2");
		query.Should().ContainKey("per_page");
		query["per_page"].Should().Be("100");
	}

	[Fact]
	public async Task GetAll_WithUpdatedFromTo_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = new HttpClient(capture)
		{
			BaseAddress = new Uri("https://api.samanage.com")
		};

		var incidentsApi = RestService.For<IIncidents>(client, new RefitSettings(
			new SystemTextJsonContentSerializer(new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
			}))
		{
			UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
		});

		await incidentsApi.GetAllAsync(new GetIncidentsRequest
		{
			Layout = ResponseLayout.Long,
			UpdatedFrom = new DateTime(2023, 11, 29, 8, 0, 0),
			UpdatedTo = new DateTime(2023, 11, 30, 8, 0, 0),
		}, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		var query = ParseQuery(capture.LastRequest!.RequestUri!);

		query.Should().ContainKey("layout");
		query["layout"].Should().Be("long");
		query.Should().ContainKey("updated_from");
		query["updated_from"].Should().Be("Nov 29 2023");
		query.Should().ContainKey("updated_to");
		query["updated_to"].Should().Be("Nov 30 2023");
		query.Should().NotContainKey("updated");
	}

	private static Dictionary<string, string> ParseQuery(Uri uri)
	{
		var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		var query = uri.Query;
		if (string.IsNullOrWhiteSpace(query))
		{
			return result;
		}

		foreach (var pair in query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries))
		{
			var pieces = pair.Split('=', 2);
			var key = Uri.UnescapeDataString(pieces[0]);
			var value = pieces.Length > 1 ? Uri.UnescapeDataString(pieces[1]) : string.Empty;
			result[key] = value;
		}

		return result;
	}

	private sealed class CaptureHandler : HttpMessageHandler
	{
		public HttpRequestMessage? LastRequest { get; private set; }

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			LastRequest = request;
			var response = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new StringContent("[]")
			};

			return Task.FromResult(response);
		}
	}
}