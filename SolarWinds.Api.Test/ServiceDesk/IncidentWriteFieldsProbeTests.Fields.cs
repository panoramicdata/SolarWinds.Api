using static SolarWinds.Api.Test.ServiceDesk.ProbeJson;

namespace SolarWinds.Api.Test.ServiceDesk;

public partial class IncidentWriteFieldsProbeTests
{
	/// <summary>
	/// Probes the fields whose updated value can be read straight back off the refreshed incident.
	/// </summary>
	private async Task ProbeScalarFieldsAsync(Dictionary<string, bool> results, ProbeContext context)
	{
		var timestamp = context.Timestamp;

		results[nameof(IncidentWriteFields.Name)] = await RunProbeAsync(
			nameof(IncidentWriteFields.Name),
			() => CreateProbeIncidentRequest($"name-{timestamp}"),
			_ => Task.FromResult(new IncidentWriteFields { Name = $"Updated name {timestamp}" }),
			(_, refreshed) => Task.FromResult(refreshed.Name == $"Updated name {timestamp}"));

		results[nameof(IncidentWriteFields.SiteId)] = await RunProbeAsync(
			nameof(IncidentWriteFields.SiteId),
			() => CreateProbeIncidentRequest($"site-id-{timestamp}"),
			_ => Task.FromResult(new IncidentWriteFields { SiteId = context.SiteId }),
			(_, refreshed) => Task.FromResult(TryGetJsonObjectId(refreshed.Site, out var updatedSiteId) && updatedSiteId == context.SiteId));

		results[nameof(IncidentWriteFields.DepartmentId)] = await RunProbeAsync(
			nameof(IncidentWriteFields.DepartmentId),
			() => CreateProbeIncidentRequest($"department-id-{timestamp}"),
			_ => Task.FromResult(new IncidentWriteFields { DepartmentId = context.DepartmentId }),
			(_, refreshed) => Task.FromResult(TryGetJsonObjectId(refreshed.Department, out var updatedDepartmentId) && updatedDepartmentId == context.DepartmentId));

		results[nameof(IncidentWriteFields.Description)] = await RunProbeAsync(
			nameof(IncidentWriteFields.Description),
			() => CreateProbeIncidentRequest($"description-{timestamp}"),
			_ => Task.FromResult(new IncidentWriteFields { Description = $"Updated description {timestamp}" }),
			(_, refreshed) => Task.FromResult(refreshed.Description == $"Updated description {timestamp}"));

		results[nameof(IncidentWriteFields.Priority)] = await RunProbeAsync(
			nameof(IncidentWriteFields.Priority),
			() => CreateProbeIncidentRequest($"priority-{timestamp}"),
			_ => Task.FromResult(new IncidentWriteFields { Priority = "High" }),
			(_, refreshed) => Task.FromResult(string.Equals(refreshed.Priority, "High", StringComparison.OrdinalIgnoreCase)));

		results[nameof(IncidentWriteFields.DueAt)] = await RunProbeAsync(
			nameof(IncidentWriteFields.DueAt),
			() => CreateProbeIncidentRequest($"due-at-{timestamp}"),
			_ => Task.FromResult(new IncidentWriteFields { DueAt = DateTime.UtcNow.AddDays(1) }),
			(_, refreshed) => Task.FromResult(refreshed.DueAt.HasValue));
	}

	/// <summary>
	/// Probes the two ways of setting the workflow state. Both need a target state looked up on
	/// the incident itself, because the states on offer differ per tenant and per incident.
	/// </summary>
	private async Task ProbeStateFieldsAsync(Dictionary<string, bool> results, ProbeContext context)
	{
		var timestamp = context.Timestamp;
		int? targetStateId = null;
		string? targetStateName = null;

		results[nameof(IncidentWriteFields.StateId)] = await RunProbeAsync(
			nameof(IncidentWriteFields.StateId),
			() => CreateProbeIncidentRequest($"state-id-{timestamp}"),
			async created =>
			{
				(targetStateId, targetStateName) = await GetTargetStateAsync(created.Id);
				return new IncidentWriteFields { StateId = targetStateId };
			},
			(_, refreshed) => Task.FromResult(string.Equals(refreshed.State, targetStateName, StringComparison.OrdinalIgnoreCase)));

		targetStateId = null;
		targetStateName = null;

		results[nameof(IncidentWriteFields.State)] = await RunProbeAsync(
			nameof(IncidentWriteFields.State),
			() => CreateProbeIncidentRequest($"state-{timestamp}"),
			async created =>
			{
				(targetStateId, targetStateName) = await GetTargetStateAsync(created.Id);
				return new IncidentWriteFields { State = targetStateName };
			},
			(_, refreshed) => Task.FromResult(string.Equals(refreshed.State, targetStateName, StringComparison.OrdinalIgnoreCase)));
	}

	/// <summary>
	/// Probes the fields that carry a collection or a nested object, and so come back as JSON that
	/// has to be searched rather than compared directly.
	/// </summary>
	private async Task ProbeCollectionFieldsAsync(Dictionary<string, bool> results, ProbeContext context)
	{
		var timestamp = context.Timestamp;

		results[nameof(IncidentWriteFields.CustomFieldsValues)] = await RunProbeAsync(
			nameof(IncidentWriteFields.CustomFieldsValues),
			() => CreateProbeIncidentRequest($"custom-fields-values-{timestamp}"),
			_ => Task.FromResult(new IncidentWriteFields { CustomFieldsValues = new Dictionary<string, object> { ["coverage_probe"] = timestamp } }),
			(_, refreshed) => Task.FromResult(JsonSerializer.Serialize(refreshed.CustomFieldsValues).Contains(timestamp, StringComparison.OrdinalIgnoreCase)));

		results[nameof(IncidentWriteFields.ConfigurationItemIds)] = await RunProbeAsync(
			nameof(IncidentWriteFields.ConfigurationItemIds),
			() => CreateProbeIncidentRequest($"configuration-item-ids-{timestamp}"),
			_ => Task.FromResult(new IncidentWriteFields { ConfigurationItemIds = [context.ConfigurationItemId] }),
			(_, refreshed) => Task.FromResult(TryGetJsonArrayFirstObjectId(refreshed.ConfigurationItems, out var relatedConfigurationItemId) && relatedConfigurationItemId == context.ConfigurationItemId));

		results[nameof(IncidentWriteFields.Cc)] = await RunProbeAsync(
			nameof(IncidentWriteFields.Cc),
			() => CreateProbeIncidentRequest($"cc-{timestamp}"),
			_ => Task.FromResult(new IncidentWriteFields { Cc = [$"coverage-{timestamp}@example.com"] }),
			(_, refreshed) => Task.FromResult(JsonSerializer.Serialize(refreshed.Cc).Contains($"coverage-{timestamp}@example.com", StringComparison.OrdinalIgnoreCase)));
	}
}
