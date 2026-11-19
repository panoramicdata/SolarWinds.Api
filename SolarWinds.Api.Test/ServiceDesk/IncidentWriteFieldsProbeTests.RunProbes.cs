using static SolarWinds.Api.Test.ServiceDesk.ProbeJson;

namespace SolarWinds.Api.Test.ServiceDesk;

public partial class IncidentWriteFieldsProbeTests
{
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
}
