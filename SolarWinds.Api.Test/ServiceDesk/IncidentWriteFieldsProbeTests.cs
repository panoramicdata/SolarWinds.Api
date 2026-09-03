using Microsoft.Extensions.Configuration;

namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Defines this contract.
/// </summary>
public interface ILooseIncidentUpdates
{
	/// <summary>
	/// Executes CreateAsync.
	/// </summary>
	[Post("/incidents.json")]
	public Task<Incident> CreateAsync([Body] IncidentCreateRequest request, CancellationToken cancellationToken);

	/// <summary>
	/// Executes GetRawAsync.
	/// </summary>
	[Get("/incidents/{id}.json")]
	public Task<JsonElement> GetRawAsync(int id, CancellationToken cancellationToken);

	/// <summary>
	/// Executes UpdateAsync.
	/// </summary>
	[Put("/incidents/{id}.json")]
	public Task<Incident> UpdateAsync(int id, [Body] IncidentWriteFields request, CancellationToken cancellationToken);
}

/// <summary>
/// Represents this type.
/// </summary>
public class IncidentWriteFieldsProbeTests : TestWithOutput
{
	private readonly ITestOutputHelper _output;
	private readonly ILooseIncidentUpdates _looseIncidentUpdates;

	/// <summary>
	/// The records and run-unique timestamp shared by every probe in a run.
	/// </summary>
	private sealed record ProbeContext(string Timestamp, int SiteId, int DepartmentId, int ConfigurationItemId);

	/// <summary>
	/// Initializes a new instance of the <see cref="IncidentWriteFieldsProbeTests"/> class.
	/// </summary>
	public IncidentWriteFieldsProbeTests(ITestOutputHelper output) : base(output) =>
		(_output, _looseIncidentUpdates) = (output, CreateLooseIncidentUpdatesClient());

	/// <summary>
	/// Executes IncidentWriteFields_UpdateAsync_ReportsWhichFieldsRoundTrip.
	/// </summary>
	[Fact]
	public async Task IncidentWriteFields_UpdateAsync_ReportsWhichFieldsRoundTrip()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		var context = await BuildProbeContextAsync();
		var results = new Dictionary<string, bool>(StringComparer.Ordinal);

		await ProbeScalarFieldsAsync(results, context);
		await ProbeStateFieldsAsync(results, context);
		await ProbeCollectionFieldsAsync(results, context);

		ReportProbeResults("write-field", results);

		// State and CustomFieldsValues are accepted on create but not on update, so a successful
		// round trip through update would mean the API had changed under us.
		var expectedUpdateFailures = new HashSet<string>(StringComparer.Ordinal)
		{
			nameof(IncidentWriteFields.State),
			nameof(IncidentWriteFields.CustomFieldsValues),
		};

		foreach (var result in results)
		{
			if (expectedUpdateFailures.Contains(result.Key))
			{
				result.Value.Should().BeFalse($"{result.Key} is known to be create-only and should not round-trip through update");
			}
			else
			{
				result.Value.Should().BeTrue($"{result.Key} should round-trip through update");
			}
		}
	}

	/// <summary>
	/// Reads the existing records a probe needs to reference, and mints the timestamp that makes
	/// each probe's values unique to this run.
	/// </summary>
	private async Task<ProbeContext> BuildProbeContextAsync()
	{
		var sites = await ServiceDeskClient.Sites.GetAsync(CancellationToken);
		sites.Should().NotBeEmpty();

		var departments = await ServiceDeskClient.Departments.GetAsync(CancellationToken);
		departments.Should().NotBeEmpty();

		var configurationItems = await ServiceDeskClient.ConfigurationItems.GetAsync(new GetConfigurationItemsRequest(), CancellationToken);
		configurationItems.Should().NotBeEmpty();

		return new ProbeContext(
			DateTimeOffset.UtcNow.ToString("yyyyMMddHHmmssfff"),
			sites[0].Id,
			departments[0].Id,
			configurationItems[0].Id);
	}

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

	/// <summary>
	/// Creates a probe incident, retrying a server-side failure, which these sandbox tenants
	/// return intermittently on create.
	/// </summary>
	private async Task<Incident?> TryCreateProbeIncidentAsync(string propertyName, Func<IncidentCreateRequest> createRequestFactory)
	{
		const int maxAttemptCount = 5;

		for (var attempt = 1; attempt <= maxAttemptCount; attempt++)
		{
			try
			{
				return await _looseIncidentUpdates.CreateAsync(createRequestFactory(), CancellationToken);
			}
			catch (ApiException ex) when ((int)ex.StatusCode >= 500)
			{
				if (attempt == maxAttemptCount)
				{
					_output.WriteLine($"{propertyName}: create API exception after {attempt} attempts");
				}
			}
			catch (ApiException ex)
			{
				_output.WriteLine($"{propertyName}: create API exception {(int)ex.StatusCode}");
				return null;
			}
		}

		return null;
	}

	/// <summary>
	/// Creates an incident, updates one field on it, and reports whether the change is visible on
	/// a re-read. The whole probe is attempted twice, because a first failure is often the tenant
	/// rather than the field.
	/// </summary>
	/// <param name="propertyName">Name of the field under test, used in the log output.</param>
	/// <param name="createRequestFactory">Builds the request that creates the probe incident.</param>
	/// <param name="updateRequestFactory">Builds the update to apply, given the created incident.</param>
	/// <param name="verifyUpdated">Decides whether the change is visible, given the before and after incidents.</param>
	private async Task<bool> RunProbeAsync(
		string propertyName,
		Func<IncidentCreateRequest> createRequestFactory,
		Func<Incident, Task<IncidentWriteFields>> updateRequestFactory,
		Func<Incident, Incident, Task<bool>> verifyUpdated)
	{
		if (await RunProbeAttemptAsync())
		{
			return true;
		}

		_output.WriteLine($"{propertyName}: retrying probe after initial verification failure");
		return await RunProbeAttemptAsync();

		async Task<bool> RunProbeAttemptAsync()
		{
			Incident? created = null;
			try
			{
				created = await TryCreateProbeIncidentAsync(propertyName, createRequestFactory);
				if (created is null)
				{
					return false;
				}

				var beforeUpdate = await ServiceDeskClient.Incidents.GetAsync(created.Id, ResponseLayout.Short, CancellationToken);

				try
				{
					_ = await _looseIncidentUpdates.UpdateAsync(
						created.Id,
						await updateRequestFactory(beforeUpdate),
						CancellationToken);
				}
				catch (ApiException)
				{
					_output.WriteLine($"{propertyName}: update API exception");
					return false;
				}

				var refreshed = await ServiceDeskClient.Incidents.GetAsync(created.Id, ResponseLayout.Short, CancellationToken);
				return await verifyUpdated(beforeUpdate, refreshed);
			}
			finally
			{
				if (created?.Id > 0)
				{
					await TryCleanupAsync(() => ServiceDeskClient.Incidents.DeleteAsync(created.Id, CancellationToken));
				}
			}
		}
	}

	/// <summary>
	/// Writes the tally of probe outcomes to the test output, naming the fields that failed.
	/// </summary>
	/// <param name="label">Describes which set of probes ran, for the log line.</param>
	/// <param name="results">Each probed field, and whether it behaved as the probe expected.</param>
	private void ReportProbeResults(string label, Dictionary<string, bool> results)
	{
		var failedResults = results.Where(entry => !entry.Value).Select(entry => entry.Key).ToArray();
		_output.WriteLine($"Incident {label} probes executed: {results.Count}, failures: {failedResults.Length}");
		if (failedResults.Length > 0)
		{
			_output.WriteLine($"Incident {label} probes that failed: " + string.Join(", ", failedResults));
		}
	}

	/// <summary>
	/// Executes IncidentWriteFields_CreateAsync_ReportsRemainingCreateCoverage.
	/// </summary>
	[Fact]
	public async Task IncidentWriteFields_CreateAsync_ReportsRemainingCreateCoverage()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		var results = new Dictionary<string, bool>(StringComparer.Ordinal);
		var timestamp = DateTimeOffset.UtcNow.ToString("yyyyMMddHHmmssfff");
		const string targetStateName = "New";

		results[nameof(IncidentWriteFields.State)] = await RunCreateProbeAsync(
			nameof(IncidentWriteFields.State),
			timestamp,
			fields => fields.State = targetStateName,
			json => TryGetJsonPropertyValue(json, "state", out var state) && string.Equals(state, targetStateName, StringComparison.OrdinalIgnoreCase));

		results[nameof(IncidentWriteFields.DueAt)] = await RunCreateProbeAsync(
			nameof(IncidentWriteFields.DueAt),
			timestamp,
			fields => fields.DueAt = DateTime.UtcNow.AddDays(1),
			json => TryGetJsonDateTimeByProperty(json, "due_at", out _));

		results[nameof(IncidentWriteFields.CustomFieldsValues)] = await RunCreateProbeAsync(
			nameof(IncidentWriteFields.CustomFieldsValues),
			timestamp,
			fields => fields.CustomFieldsValues = new Dictionary<string, object> { ["coverage_probe"] = timestamp },
			json => TryGetJsonProperty(json, "custom_fields_values", out var customFields) && customFields.ValueKind != JsonValueKind.Null);

		ReportProbeResults("create-field", results);

		results.Should().OnlyContain(entry => entry.Value, "remaining incident write fields should create successfully");
	}

	/// <summary>
	/// Creates an incident with one field set, and reports whether that field is present on the
	/// raw JSON the API returns for it.
	/// </summary>
	/// <param name="propertyName">Name of the field under test, used in the incident name.</param>
	/// <param name="timestamp">Run-unique suffix that keeps the probe incidents apart.</param>
	/// <param name="mutate">Sets the field under test on the create request.</param>
	/// <param name="verifyCreated">Decides whether the field survived, given the raw incident JSON.</param>
	private async Task<bool> RunCreateProbeAsync(
		string propertyName,
		string timestamp,
		Action<IncidentWriteFields> mutate,
		Func<JsonElement, bool> verifyCreated)
	{
		Incident? created = null;
		try
		{
			try
			{
				var request = CreateProbeIncidentRequest($"create-{propertyName}-{timestamp}");
				mutate(request.Incident);
				created = await _looseIncidentUpdates.CreateAsync(request, CancellationToken);
			}
			catch (ApiException)
			{
				return false;
			}

			var raw = await _looseIncidentUpdates.GetRawAsync(created.Id, CancellationToken);
			return verifyCreated(raw);
		}
		finally
		{
			if (created?.Id > 0)
			{
				await TryCleanupAsync(() => ServiceDeskClient.Incidents.DeleteAsync(created.Id, CancellationToken));
			}
		}
	}

	private static bool ShouldRunDestructiveIntegrationTests()
	{
		var configuration = GetCoverageConfiguration();

		var runCoverage = bool.TryParse(configuration["ServiceDesk:Coverage:RunDestructiveTests"], out var explicitRun) && explicitRun;
		var runLifecycle = bool.TryParse(configuration["ServiceDesk:Lifecycle:RunTests"], out var lifecycleRun) && lifecycleRun;
		if (!runCoverage && !runLifecycle)
		{
			return false;
		}

		var baseUrl = configuration["ServiceDesk:BaseUrl"] ?? string.Empty;
		return baseUrl.Contains("panoramicdatalimited.samanage.com", StringComparison.OrdinalIgnoreCase);
	}

	private static IConfiguration GetCoverageConfiguration() => new ConfigurationBuilder()
			.AddUserSecrets<IncidentWriteFieldsProbeTests>()
			.Build();

	private static ILooseIncidentUpdates CreateLooseIncidentUpdatesClient()
	{
		var configuration = GetCoverageConfiguration();
		var baseUrl = configuration["ServiceDesk:BaseUrl"] ?? throw new InvalidOperationException("ServiceDesk:BaseUrl is missing in User Secrets.");
		var accessToken = configuration["ServiceDesk:AccessToken"] ?? throw new InvalidOperationException("ServiceDesk:AccessToken is missing in User Secrets.");

		var httpClient = new HttpClient
		{
			BaseAddress = new Uri(baseUrl, UriKind.Absolute)
		};

		httpClient.DefaultRequestHeaders.Add("X-Samanage-Authorization", "Bearer " + accessToken);
		httpClient.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.samanage.v2.1+json");

		return RestService.For<ILooseIncidentUpdates>(
			httpClient,
			new RefitSettings(new SystemTextJsonContentSerializer(new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
				DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
			})));
	}

	private static IncidentCreateRequest CreateProbeIncidentRequest(string suffix) => new()
	{
		Incident = new IncidentWriteFields
		{
			Name = $"Coverage incident {suffix}",
			Description = $"Coverage incident {suffix}",
			Priority = "Low",
		}
	};

	/// <summary>
	/// The states a probe prefers to move an incident to, most preferred first. All of them leave
	/// the incident open, so the probe can still delete it afterwards.
	/// </summary>
	private static readonly string[] PreferredTargetStateNames =
		["Awaiting Input", "On Hold", "Assigned", "In Progress", "New"];

	private async Task<(int Id, string Name)> GetTargetStateAsync(int incidentId)
	{
		var generalInfo = await ServiceDeskClient.Incidents.GetEntityGeneralInfoAsync(incidentId, CancellationToken);

		var candidates = generalInfo.States
			.Where(state => !state.Selected && !state.Archived)
			.ToList();

		var targetState = FindPreferredTargetState(candidates)
			?? candidates.FirstOrDefault()
			?? generalInfo.States.First();

		return (targetState.Id, targetState.Title ?? targetState.Key ?? targetState.Id.ToString());
	}

	/// <summary>
	/// Picks the most preferred of the offered states, falling back to any state that is neither
	/// Resolved nor Closed.
	/// </summary>
	/// <param name="candidates">The states the incident can currently be moved to.</param>
	private static IncidentEntityGeneralInfoState? FindPreferredTargetState(List<IncidentEntityGeneralInfoState> candidates)
		=> PreferredTargetStateNames
			.Select(name => candidates.FirstOrDefault(state => StateMatches(state, name)))
			.FirstOrDefault(state => state is not null)
			?? candidates.FirstOrDefault(state => !StateMatches(state, "Resolved") && !StateMatches(state, "Closed"));

	/// <summary>
	/// Whether a state goes by the given name, which tenants put on either the key or the title.
	/// </summary>
	/// <param name="state">The state to test.</param>
	/// <param name="value">The name to match.</param>
	private static bool StateMatches(IncidentEntityGeneralInfoState state, string value)
		=> string.Equals(state.Key, value, StringComparison.OrdinalIgnoreCase)
			|| string.Equals(state.Title, value, StringComparison.OrdinalIgnoreCase);

	private static bool TryGetJsonObjectId(object? value, out int id)
	{
		id = 0;

		if (value is null)
		{
			return false;
		}

		using var document = JsonDocument.Parse(JsonSerializer.Serialize(value));
		var root = document.RootElement;
		if (root.ValueKind != JsonValueKind.Object)
		{
			return false;
		}

		return TryGetJsonPropertyInt32(root, "id", out id) || TryGetJsonPropertyInt32(root, "Id", out id);
	}

	private static bool TryGetJsonArrayFirstObjectId(object? value, out int id)
	{
		id = 0;

		if (value is null)
		{
			return false;
		}

		using var document = JsonDocument.Parse(JsonSerializer.Serialize(value));
		var root = document.RootElement;
		if (root.ValueKind != JsonValueKind.Array || root.GetArrayLength() == 0)
		{
			return false;
		}

		var first = root[0];
		return first.ValueKind == JsonValueKind.Object
			&& (TryGetJsonPropertyInt32(first, "id", out id) || TryGetJsonPropertyInt32(first, "Id", out id));
	}

	private static bool TryGetJsonPropertyInt32(JsonElement element, string propertyName, out int value)
	{
		foreach (var property in element.EnumerateObject())
		{
			if (string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase)
				&& property.Value.TryGetInt32(out value))
			{
				return true;
			}
		}

		value = 0;
		return false;
	}

	private static bool TryGetJsonProperty(JsonElement element, string propertyName, out JsonElement value)
	{
		foreach (var property in element.EnumerateObject())
		{
			if (string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
			{
				value = property.Value;
				return true;
			}
		}

		value = default;
		return false;
	}

	private static bool TryGetJsonPropertyValue(JsonElement element, string propertyName, out string value)
	{
		if (TryGetJsonProperty(element, propertyName, out var propertyValue)
			&& propertyValue.ValueKind == JsonValueKind.String
			&& propertyValue.GetString() is { } propertyString)
		{
			value = propertyString;
			return true;
		}

		value = string.Empty;
		return false;
	}

	private static bool TryGetJsonDateTimeByProperty(JsonElement element, string propertyName, out DateTime value)
	{
		if (TryGetJsonProperty(element, propertyName, out var propertyValue)
			&& propertyValue.ValueKind == JsonValueKind.String
			&& propertyValue.TryGetDateTime(out value))
		{
			return true;
		}

		value = default;
		return false;
	}
}
