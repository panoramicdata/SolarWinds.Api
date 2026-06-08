using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class IncidentQueryIntegrationTests(ITestOutputHelper output) : TestWithOutput(output)
{
	/// <summary>
	/// Executes GetAll_WithLayoutLong_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_WithLayoutLong_ReturnsItems()
	{
		var items = await QueryAsync(r => r.Layout = ResponseLayout.Long);
		items.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetAll_WithUpdatedSelector_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_WithUpdatedSelector_ReturnsItems()
	{
		var items = await QueryAsync(r => r.Updated = "7");
		items.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetAll_WithUpdatedCustomRange_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_WithUpdatedCustomRange_ReturnsItems()
	{
		var from = DateTime.UtcNow.Date.AddDays(-14);
		var to = DateTime.UtcNow.Date.AddDays(-7);

		var items = await QueryAsync(r =>
		{
			r.Updated = "Select Date Range";
			r.UpdatedCustomGte = from;
			r.UpdatedCustomLte = to;
		});

		items.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetAll_WithUpdatedFrom_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_WithUpdatedFrom_ReturnsItems()
	{
		var items = await QueryAsync(r => r.UpdatedFrom = DateTime.UtcNow.Date.AddDays(-14));
		items.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetAll_WithUpdatedTo_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_WithUpdatedTo_ReturnsItems()
	{
		var from = DateTime.UtcNow.Date.AddDays(-14);
		var to = DateTime.UtcNow.Date.AddDays(-1);

		var items = await QueryAsync(r =>
		{
			r.UpdatedFrom = from;
			r.UpdatedTo = to;
		});
		items.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetAll_WithCreatedFrom_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_WithCreatedFrom_ReturnsItems()
	{
		var items = await QueryAsync(r => r.CreatedFrom = DateTime.UtcNow.Date.AddDays(-30));
		items.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetAll_WithCreatedTo_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_WithCreatedTo_ReturnsItems()
	{
		var from = DateTime.UtcNow.Date.AddDays(-30);
		var to = DateTime.UtcNow.Date.AddDays(-1);

		var items = await QueryAsync(r =>
		{
			r.CreatedFrom = from;
			r.CreatedTo = to;
		});
		items.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetAll_WithPage_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_WithPage_ReturnsItems()
	{
		var items = await QueryAsync(r => r.Page = 1);
		items.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetAll_WithPerPage_ReturnsItemsLimitedByPerPage.
	/// </summary>
	[Fact]
	public async Task GetAll_WithPerPage_ReturnsItemsLimitedByPerPage()
	{
		const int perPage = 1;
		var items = await QueryAsync(r => r.PerPage = perPage);

		items.Should().NotBeNull();
		items.Count.Should().BeLessThanOrEqualTo(perPage);
	}

	/// <summary>
	/// Executes GetAll_WithTitleQueryUsingIncidentRequest_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_WithTitleQueryUsingIncidentRequest_ReturnsItems()
	{
		const string problemSignature = "New Laptop Request";

		var request = new GetIncidentsRequest
		{
			Layout = ResponseLayout.Short,
			Title = [problemSignature]
		};

		var items = await ServiceDeskClient
			.Incidents
			.GetAsync(request, CancellationToken);

		items.Should().NotBeNull();
	}

	/// <summary>
	/// Executes GetAll_WithAllSupportedParameters_ReturnsItems.
	/// </summary>
	[Fact]
	public async Task GetAll_WithAllSupportedParameters_ReturnsItems()
	{
		var from = DateTime.UtcNow.Date.AddDays(-14);
		var to = DateTime.UtcNow.Date.AddDays(-7);

		var items = await QueryAsync(r =>
		{
			r.Layout = ResponseLayout.Long;
			r.Updated = "Select Date Range";
			r.UpdatedCustomGte = from;
			r.UpdatedCustomLte = to;
			r.UpdatedFrom = from;
			r.UpdatedTo = to;
			r.CreatedFrom = from.AddDays(-14);
			r.CreatedTo = to;
			r.Page = 1;
			r.PerPage = 10;
		});

		items.Should().NotBeNull();
	}

	private async Task<List<Incident>> QueryAsync(Action<GetIncidentsRequest> configure)
	{
		var request = new GetIncidentsRequest
		{
			Layout = ResponseLayout.Short,
		};

		configure(request);

		return await ServiceDeskClient.Incidents.GetAsync(request, CancellationToken);
	}

	private static async Task<List<Incident>> QueryIncidentsByCustomFieldAsync(HttpClient httpClient, int customFieldId, string value)
	{
		var key = Uri.EscapeDataString($"{customFieldId}[]");
		var encodedValue = Uri.EscapeDataString(value);
		var response = await httpClient.GetAsync($"/incidents.json?layout=long&per_page=100&{key}={encodedValue}", CancellationToken);
		response.EnsureSuccessStatusCode();

		await using var stream = await response.Content.ReadAsStreamAsync(CancellationToken);

		var incidents = await JsonSerializer.DeserializeAsync<List<Incident>>(stream, SnakeCaseLowerJsonSerializerOptions, CancellationToken);

		return incidents ?? [];
	}

	private static HttpClient CreateServiceDeskHttpClient()
	{
		var configuration = new ConfigurationBuilder()
			.AddUserSecrets<IncidentQueryIntegrationTests>()
			.Build();

		var baseUrl = configuration["ServiceDesk:BaseUrl"] ?? string.Empty;
		var accessToken = configuration["ServiceDesk:AccessToken"] ?? string.Empty;
		if (string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(accessToken))
		{
			throw new InvalidOperationException("ServiceDesk credentials are missing from User Secrets.");
		}

		var client = new HttpClient
		{
			BaseAddress = new Uri(baseUrl)
		};
		client.DefaultRequestHeaders.Add("X-Samanage-Authorization", "Bearer " + accessToken);
		client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.samanage.v2.1+json");
		return client;
	}

	private static string? TryReadCustomFieldValue(object? customFieldsValues, int customFieldId)
	{
		if (customFieldsValues is not JsonElement customFieldsElement || customFieldsElement.ValueKind != JsonValueKind.Object)
		{
			return null;
		}

		if (!customFieldsElement.TryGetProperty(customFieldId.ToString(CultureInfo.InvariantCulture), out var fieldValueElement))
		{
			return null;
		}

		return ExtractStringValue(fieldValueElement);
	}

	private static string? ExtractStringValue(JsonElement value)
	{
		if (value.ValueKind == JsonValueKind.String)
		{
			return value.GetString();
		}

		if (value.ValueKind == JsonValueKind.Number || value.ValueKind == JsonValueKind.True || value.ValueKind == JsonValueKind.False)
		{
			return value.ToString();
		}

		if (value.ValueKind == JsonValueKind.Object && value.TryGetProperty("value", out var nestedValue))
		{
			return ExtractStringValue(nestedValue);
		}

		if (value.ValueKind == JsonValueKind.Array && value.GetArrayLength() > 0)
		{
			return ExtractStringValue(value[0]);
		}

		return null;
	}
}
