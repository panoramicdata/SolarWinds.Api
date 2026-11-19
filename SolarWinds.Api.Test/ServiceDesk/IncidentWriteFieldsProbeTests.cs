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
[Trait("Category", "Integration")]
public partial class IncidentWriteFieldsProbeTests : TestWithOutput
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
		if (!DestructiveTestGate.ShouldRun())
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
}
