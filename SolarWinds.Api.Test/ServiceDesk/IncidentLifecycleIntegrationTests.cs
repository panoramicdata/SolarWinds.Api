using System;
using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test.ServiceDesk;

public class IncidentLifecycleIntegrationTests(ITestOutputHelper iTestOutputHelper) : TestWithOutput(iTestOutputHelper)
{
	[Fact]
	public async Task Create_Update_Close_TestIncident()
	{
		if (!ShouldRunIntegrationTest())
		{
			return;
		}

		var config = LoadLifecycleConfig();

		var externalId = DateTimeOffset.UtcNow.ToString("yyyyMMddHHmmss");
		var ticketNumber = $"MS-TEST-{externalId}";
		var ticketName = $"[Safe Test] SolarWinds API validation {externalId}";

		var incident = new SolarWinds.Api.ServiceDesk.Models.Incident
		{
			Name = ticketName,
			Description = $"Safe integration test ticket created by SolarWinds.Api.Test at {DateTimeOffset.UtcNow:O}",
			Assignee = config.Assignee,
			Requester = config.Requester,
			Number = ticketNumber,
			Priority = config.Priority,
			Category = config.Category,
			Subcategory = config.Subcategory,
			GroupAssignee = config.GroupAssignee,
			TagList = config.TagList
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

		updated.StateId = config.ClosedStateId;
		var closed = await ServiceDeskClient
			.Incidents
			.UpdateAsync(created.Id, updated, CancellationToken);

		closed.StateId.Should().Be(config.ClosedStateId);
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

		var config = new ServiceDeskLifecycleTestConfig
		{
			Requester = configuration["ServiceDesk:Lifecycle:Requester"] ?? string.Empty,
			Assignee = configuration["ServiceDesk:Lifecycle:Assignee"] ?? string.Empty,
			GroupAssignee = configuration["ServiceDesk:Lifecycle:GroupAssignee"] ?? string.Empty,
			Category = configuration["ServiceDesk:Lifecycle:Category"] ?? string.Empty,
			Subcategory = configuration["ServiceDesk:Lifecycle:Subcategory"] ?? string.Empty,
			Priority = configuration["ServiceDesk:Lifecycle:Priority"] ?? "Low",
			TagList = configuration["ServiceDesk:Lifecycle:TagList"] ?? "integration-test,non-intrusive",
		};

		if (!int.TryParse(configuration["ServiceDesk:Lifecycle:ClosedStateId"], out var closedStateId) || closedStateId <= 0)
		{
			throw new InvalidOperationException(
				"ServiceDesk:Lifecycle:ClosedStateId is missing or invalid in User Secrets.");
		}

		config.ClosedStateId = closedStateId;

		if (string.IsNullOrWhiteSpace(config.Requester) ||
			string.IsNullOrWhiteSpace(config.Assignee) ||
			string.IsNullOrWhiteSpace(config.GroupAssignee) ||
			string.IsNullOrWhiteSpace(config.Category) ||
			string.IsNullOrWhiteSpace(config.Subcategory))
		{
			throw new InvalidOperationException(
				"ServiceDesk lifecycle test settings are incomplete in User Secrets. " +
				"Set keys under ServiceDesk:Lifecycle:* (Requester, Assignee, GroupAssignee, Category, Subcategory, ClosedStateId)."
			);
		}

		return config;
	}

	private sealed class ServiceDeskLifecycleTestConfig
	{
		public string Requester { get; set; } = string.Empty;
		public string Assignee { get; set; } = string.Empty;
		public string GroupAssignee { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
		public string Subcategory { get; set; } = string.Empty;
		public string Priority { get; set; } = "Low";
		public string TagList { get; set; } = "integration-test,non-intrusive";
		public int ClosedStateId { get; set; }
	}
}
