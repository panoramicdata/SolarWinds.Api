using System;
using Microsoft.Extensions.Logging;

namespace SolarWinds.Api;

/// <summary>
/// Options for the SolarWinds Service Desk client.
/// </summary>
public class SolarWindsServiceDeskClientOptions
{
	/// <summary>
	/// The base URL for the SolarWinds Service Desk API (e.g., "https://api.samanage.com").
	/// </summary>
	public string BaseUrl { get; set; } = string.Empty;

	/// <summary>
	/// The access token for authentication.
	/// </summary>
	public string AccessToken { get; set; } = string.Empty;

	/// <summary>
	/// Optional logger. When provided, all HTTP request and response details are logged at Debug level.
	/// </summary>
	public ILogger? Logger { get; set; }

	/// <summary>
	/// The maximum number of times to attempt a request before returning the final response.
	/// </summary>
	public int MaxAttemptCount { get; set; } = 5;

	/// <summary>
	/// The exponential backoff factor used when the API does not provide an explicit retry delay.
	/// </summary>
	public double BackOffDelayFactor { get; set; } = 2;

	/// <summary>
	/// The maximum automatic backoff delay in seconds.
	/// </summary>
	public int MaxBackOffDelaySeconds { get; set; } = 60;

	/// <summary>
	/// The underlying <see cref="HttpClient"/> timeout. Defaults to infinite so handler-managed backoff can complete.
	/// </summary>
	public TimeSpan HttpClientTimeout { get; set; } = Timeout.InfiniteTimeSpan;
}