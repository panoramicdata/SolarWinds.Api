namespace SolarWinds.Api.Http;

internal static class ServiceDeskRateLimitHeaders
{
	internal static TimeSpan GetRetryAfterDelay(HttpResponseMessage response)
	{
		var retryAfter = response.Headers.RetryAfter;
		if (retryAfter?.Delta is TimeSpan delta)
		{
			return delta < TimeSpan.Zero ? TimeSpan.Zero : delta;
		}

		if (retryAfter?.Date is DateTimeOffset date)
		{
			var delay = date - DateTimeOffset.UtcNow;
			return delay < TimeSpan.Zero ? TimeSpan.Zero : delay;
		}

		return TimeSpan.Zero;
	}

	internal static bool TryGetResetDelay(HttpResponseMessage response, out TimeSpan delay)
	{
		if (!TryGetHeaderLong(response, "X-Ratelimit-Reset", out var resetEpoch))
		{
			delay = TimeSpan.Zero;
			return false;
		}

		var nowEpoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		delay = TimeSpan.FromSeconds(Math.Max(resetEpoch - nowEpoch, 0));
		return true;
	}

	internal static bool TryGetHeaderInt(HttpResponseMessage response, string headerName, out int value)
		=> TryGetHeader(response, headerName, int.TryParse, out value);

	internal static bool TryGetHeaderLong(HttpResponseMessage response, string headerName, out long value)
		=> TryGetHeader(response, headerName, long.TryParse, out value);

	private static bool TryGetHeader<T>(HttpResponseMessage response, string headerName, TryParseHandler<T> tryParse, out T value)
	{
		if (TryGetHeaderValues(response, headerName, out var headerValue) && tryParse(headerValue, out value))
		{
			return true;
		}

		value = default!;
		return false;
	}

	private static bool TryGetHeaderValues(HttpResponseMessage response, string headerName, out string value)
	{
		if (response.Headers.TryGetValues(headerName, out var headerValues))
		{
			value = headerValues.FirstOrDefault() ?? string.Empty;
			return !string.IsNullOrWhiteSpace(value);
		}

		if (response.Content.Headers.TryGetValues(headerName, out headerValues))
		{
			value = headerValues.FirstOrDefault() ?? string.Empty;
			return !string.IsNullOrWhiteSpace(value);
		}

		value = string.Empty;
		return false;
	}

	internal static TimeSpan Max(params TimeSpan[] values) => values.Max();

	private delegate bool TryParseHandler<T>(string input, out T value);
}
