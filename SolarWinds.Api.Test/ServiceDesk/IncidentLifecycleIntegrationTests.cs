using AwesomeAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test.ServiceDesk;

public class IncidentLifecycleIntegrationTests(ITestOutputHelper output) : TestWithOutput(output)
{
	//[Fact]
	//public async Task Discover_IncidentStates()
	//{
	//	// Fetch multiple pages to catch a variety of states
	//	var allIncidents = new List<SolarWinds.Api.ServiceDesk.Models.Incident>();
	//	for (var page = 1; page <= 3; page++)
	//	{
	//		var page_incidents = await ServiceDeskClient.Incidents.GetPageAsync(page, perPage: 100, CancellationToken);
	//		allIncidents.AddRange(page_incidents);
	//		if (page_incidents.Count < 100)
	//		{
	//			break;
	//		}
	//	}

	//	output.WriteLine($"Fetched {allIncidents.Count} incidents total");

	//	var states = allIncidents
	//		.GroupBy(i => i.State ?? "(null)")
	//		.Select(g => new { State = g.Key, Count = g.Count() })
	//		.OrderBy(s => s.State);

	//	foreach (var s in states)
	//	{
	//		output.WriteLine($"State={s.State,-20} Count={s.Count}");
	//	}

	//	// Also query the /states.json endpoint directly for the full list
	//	output.WriteLine("\n-- Raw /states.json query --");
	//	using var http = new HttpClient();
	//	var accessToken = new ConfigurationBuilder()
	//		.AddUserSecrets<IncidentLifecycleIntegrationTests>()
	//		.Build()["ServiceDesk:AccessToken"]!;
	//	http.DefaultRequestHeaders.Add("X-Samanage-Authorization", "Bearer " + accessToken);
	//	http.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.samanage.v2.1+json");
	//	var json = await http.GetStringAsync("https://api.samanage.com/states.json", CancellationToken);
	//	output.WriteLine(json);

	//	var resultsPath = Path.Combine(Path.GetTempPath(), "incident_states.txt");
	//	var lines = states
	//		.Select(s => $"State={s.State,-20} Count={s.Count}")
	//		.Prepend($"Total incidents: {allIncidents.Count}")
	//		.Append("\n-- /states.json --")
	//		.Append(json);
	//	await File.WriteAllLinesAsync(resultsPath, lines, CancellationToken);

	//	allIncidents.Should().NotBeNull();
	//}

	[Fact]
	public async Task Create_Update_Close_TestIncident()
	{
		if (!ShouldRunIntegrationTest())
		{
			return;
		}

		var config = LoadLifecycleConfig();

		var incident = new SolarWinds.Api.ServiceDesk.Models.Incident
		{
			Name = config.Name,
			Description = config.Description,
			DescriptionNoHtml = config.DescriptionNoHtml,
			State = config.State,
			Priority = config.Priority,
			Category = new { id = config.CategoryId },
			Subcategory = (object?)null,
			Assignee = new { id = config.AssigneeId },
			Requester = new { id = config.RequesterId },
			IsServiceRequest = config.IsServiceRequest,
			Origin = config.Origin,
			CustomFieldsValues = new[]
			{
				new
				{
					id = config.CustomFieldValueId,
					custom_field_id = config.CustomFieldId,
					name = config.CustomFieldName,
					value = config.CustomFieldValue,
					attachment = (object?)null,
					options = config.CustomFieldOptions,
					type = config.CustomFieldType,
					type_name = config.CustomFieldTypeName,
					entity = (object?)null,
					user = (object?)null
				}
			}
		};

		var created = await ServiceDeskClient
			.Incidents
			.CreateAsync(incident, CancellationToken);

		created.Id.Should().BePositive("ticket creation should return a valid incident id");

		created.Description = $"Updated by safe integration test at {DateTimeOffset.UtcNow:O}";
		var updated = await ServiceDeskClient
			.Incidents
			.UpdateAsync(created.Id, created, CancellationToken);

		updated.Description.Should().Contain("Updated by safe integration test");

		updated.State = config.ClosedState;
		var closed = await ServiceDeskClient
			.Incidents
			.UpdateAsync(created.Id, updated, CancellationToken);

		closed.State.Should().Be(config.ClosedState);
	}

	private static bool ShouldRunIntegrationTest()
	{
		var configuration = new ConfigurationBuilder()
			.AddUserSecrets<IncidentLifecycleIntegrationTests>()
			.Build();

		return bool.TryParse(configuration["ServiceDesk:Lifecycle:RunTests"], out var shouldRun)
			&& shouldRun;
	}

	private static ServiceDeskLifecycleTestConfig LoadLifecycleConfig()
	{
		var configuration = new ConfigurationBuilder()
			.AddUserSecrets<IncidentLifecycleIntegrationTests>()
			.Build();

		static int RequireInt(IConfiguration cfg, string key) =>
			int.TryParse(cfg[key], out var v) && v > 0
				? v
				: throw new InvalidOperationException($"{key} is missing or invalid in User Secrets.");

		return new ServiceDeskLifecycleTestConfig
		{
			ClosedStateId = RequireInt(configuration, "ServiceDesk:Lifecycle:ClosedStateId"),
			CategoryId = RequireInt(configuration, "ServiceDesk:Lifecycle:CategoryId"),
			AssigneeId = RequireInt(configuration, "ServiceDesk:Lifecycle:AssigneeId"),
			RequesterId = RequireInt(configuration, "ServiceDesk:Lifecycle:RequesterId"),
			CustomFieldValueId = RequireInt(configuration, "ServiceDesk:Lifecycle:CustomFieldValueId"),
			CustomFieldId = RequireInt(configuration, "ServiceDesk:Lifecycle:CustomFieldId"),
			CustomFieldType = RequireInt(configuration, "ServiceDesk:Lifecycle:CustomFieldType"),
			Name = configuration["ServiceDesk:Lifecycle:Name"] ?? throw new InvalidOperationException("ServiceDesk:Lifecycle:Name is missing in User Secrets."),
			Description = configuration["ServiceDesk:Lifecycle:Description"] ?? throw new InvalidOperationException("ServiceDesk:Lifecycle:Description is missing in User Secrets."),
			DescriptionNoHtml = configuration["ServiceDesk:Lifecycle:DescriptionNoHtml"] ?? throw new InvalidOperationException("ServiceDesk:Lifecycle:DescriptionNoHtml is missing in User Secrets."),
			State = configuration["ServiceDesk:Lifecycle:State"] ?? throw new InvalidOperationException("ServiceDesk:Lifecycle:State is missing in User Secrets."),
			Priority = configuration["ServiceDesk:Lifecycle:Priority"] ?? throw new InvalidOperationException("ServiceDesk:Lifecycle:Priority is missing in User Secrets."),
			Origin = configuration["ServiceDesk:Lifecycle:Origin"] ?? throw new InvalidOperationException("ServiceDesk:Lifecycle:Origin is missing in User Secrets."),
			CustomFieldName = configuration["ServiceDesk:Lifecycle:CustomFieldName"] ?? throw new InvalidOperationException("ServiceDesk:Lifecycle:CustomFieldName is missing in User Secrets."),
			CustomFieldValue = configuration["ServiceDesk:Lifecycle:CustomFieldValue"] ?? throw new InvalidOperationException("ServiceDesk:Lifecycle:CustomFieldValue is missing in User Secrets."),
			CustomFieldOptions = configuration["ServiceDesk:Lifecycle:CustomFieldOptions"] ?? throw new InvalidOperationException("ServiceDesk:Lifecycle:CustomFieldOptions is missing in User Secrets."),
			CustomFieldTypeName = configuration["ServiceDesk:Lifecycle:CustomFieldTypeName"] ?? throw new InvalidOperationException("ServiceDesk:Lifecycle:CustomFieldTypeName is missing in User Secrets."),
			IsServiceRequest = bool.TryParse(configuration["ServiceDesk:Lifecycle:IsServiceRequest"], out var isSR) && isSR,
		};
	}

	private sealed class ServiceDeskLifecycleTestConfig
	{
		public int ClosedStateId { get; set; }
		public int CategoryId { get; set; }
		public int AssigneeId { get; set; }
		public int RequesterId { get; set; }
		public int CustomFieldValueId { get; set; }
		public int CustomFieldId { get; set; }
		public int CustomFieldType { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string DescriptionNoHtml { get; set; } = string.Empty;
		public string State { get; set; } = string.Empty;
		public string Priority { get; set; } = string.Empty;
		public string Origin { get; set; } = string.Empty;
		public string CustomFieldName { get; set; } = string.Empty;
		public string CustomFieldValue { get; set; } = string.Empty;
		public string CustomFieldOptions { get; set; } = string.Empty;
		public string CustomFieldTypeName { get; set; } = string.Empty;
		public bool IsServiceRequest { get; set; }
	}
}

