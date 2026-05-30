namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents service monitor statistics for an incident.
/// </summary>
public class ServiceMonitorStatistic
{
	/// <summary>
	/// Gets or sets the SLA breach status.
	/// </summary>
	[JsonPropertyName("sla_breach")]
	public bool SlaBreach { get; set; }

	/// <summary>
	/// Gets or sets the response due datetime.
	/// </summary>
	[JsonPropertyName("response_due_at")]
	public DateTime? ResponseDueAt { get; set; }

	/// <summary>
	/// Gets or sets the resolution due datetime.
	/// </summary>
	[JsonPropertyName("resolution_due_at")]
	public DateTime? ResolutionDueAt { get; set; }

	/// <summary>
	/// Gets or sets the time elapsed in seconds since the incident was created.
	/// </summary>
	[JsonPropertyName("time_elapsed")]
	public int? TimeElapsed { get; set; }

	/// <summary>
	/// Gets or sets the SLA policy name.
	/// </summary>
	[JsonPropertyName("sla_policy_name")]
	public string? SlaPolicyName { get; set; }

	/// <summary>
	/// Gets or sets additional raw statistic data.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}
