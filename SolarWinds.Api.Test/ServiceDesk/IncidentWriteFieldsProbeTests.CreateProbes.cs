using static SolarWinds.Api.Test.ServiceDesk.ProbeJson;

namespace SolarWinds.Api.Test.ServiceDesk;

public partial class IncidentWriteFieldsProbeTests
{
	/// <summary>
	/// Executes IncidentWriteFields_CreateAsync_ReportsRemainingCreateCoverage.
	/// </summary>
	[Fact]
	public async Task IncidentWriteFields_CreateAsync_ReportsRemainingCreateCoverage()
	{
		if (!DestructiveTestGate.ShouldRun())
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

	private static ILooseIncidentUpdates CreateLooseIncidentUpdatesClient()
	{
		var configuration = DestructiveTestGate.Configuration();
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
}
