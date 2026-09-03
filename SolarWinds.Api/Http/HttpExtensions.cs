using System.Net.Http.Headers;
using System.Text;

namespace SolarWinds.Api.Http;

/// <summary>
/// Rendering of HTTP headers for diagnostic output, with credential-bearing values redacted.
/// </summary>
internal static class HttpExtensions
{
	/// <summary>
	/// Header names whose values carry a credential and must never be rendered into a log message or
	/// an exception message.
	/// </summary>
	private static readonly HashSet<string> SensitiveHeaderNames = new(StringComparer.OrdinalIgnoreCase)
	{
		"Authorization",
		"Proxy-Authorization",
		"Cookie",
		"Set-Cookie",
		"X-API-Key",
		"Api-Key",
		"X-Api-Token",
		"X-Auth-Token",
	};

	/// <summary>
	/// The subset of sensitive headers whose value is of the form "&lt;scheme&gt; &lt;credential&gt;",
	/// where the scheme is safe to keep and useful to see.
	/// </summary>
	private static readonly HashSet<string> SchemePrefixedHeaderNames = new(StringComparer.OrdinalIgnoreCase)
	{
		"Authorization",
		"Proxy-Authorization",
	};

	/// <summary>
	/// Whether a header name denotes a credential-bearing header.
	/// </summary>
	/// <remarks>
	/// The suffix test is what covers this client's own credential. Service Desk authenticates with
	/// "X-Samanage-Authorization", so an exact-match list copied from a sibling package would render
	/// the access token verbatim while appearing to be fixed.
	/// </remarks>
	private static bool IsSensitive(string name)
		=> SensitiveHeaderNames.Contains(name)
		|| name.EndsWith("Authorization", StringComparison.OrdinalIgnoreCase);

	/// <summary>
	/// Whether a header's grammar is "&lt;scheme&gt; &lt;credential&gt;", so its scheme can be kept.
	/// </summary>
	private static bool IsSchemePrefixed(string name)
		=> SchemePrefixedHeaderNames.Contains(name)
		|| name.EndsWith("Authorization", StringComparison.OrdinalIgnoreCase);

	/// <summary>
	/// Joins a header's values, replacing the credential with a redaction marker when the header is a
	/// sensitive one.
	/// </summary>
	/// <remarks>
	/// The authentication scheme and the credential length are preserved. That is enough to tell an
	/// engineer that a credential was sent and roughly what shape it had, which is all diagnosis needs,
	/// without writing the credential itself somewhere it will be retained and widely readable.
	/// </remarks>
	internal static string RedactIfSensitive(string name, IEnumerable<string> values)
	{
		var value = string.Join(", ", values);

		if (value.Length == 0 || !IsSensitive(name))
		{
			return value;
		}

		// Only headers whose grammar is "<scheme> <credential>" keep their scheme, so that which
		// authentication mechanism was used remains visible. Applying this to any header containing a
		// space would be unsafe: a cookie such as "session=abc123; HttpOnly" also contains one, and
		// treating the text before it as a scheme would preserve the very value being redacted.
		if (IsSchemePrefixed(name))
		{
			var schemeLength = value.IndexOf(' ', StringComparison.Ordinal);

			if (schemeLength > 0)
			{
				return $"{value[..schemeLength]} <redacted, length {value.Length - schemeLength - 1}>";
			}
		}

		return $"<redacted, length {value.Length}>";
	}

	/// <summary>
	/// Appends each header as an indented "Name: value" line, redacting credential-bearing values.
	/// </summary>
	internal static void AppendRedacted(this StringBuilder stringBuilder, HttpHeaders headers)
	{
		foreach (var header in headers)
		{
			stringBuilder.AppendLine($"  {header.Key}: {RedactIfSensitive(header.Key, header.Value)}");
		}
	}
}
