using System.Net.Http;
using System.Net.Http.Headers;
using SolarWinds.Api.Http;

namespace SolarWinds.Api.Test.Http;

/// <summary>
/// Tests for header redaction in diagnostic output.
///
/// <para>
/// <see cref="LoggingDelegatingHandler"/> writes every request and response header into a Debug
/// level log message. <see cref="SolarWindsServiceDeskClient"/> authenticates by adding
/// "X-Samanage-Authorization: Bearer &lt;token&gt;" to the client's default request headers, which
/// HttpClient merges into every request before the handler pipeline runs. Without redaction a usable
/// access token is therefore written wherever those messages end up.
/// </para>
///
/// <para>
/// These are pure unit tests. They construct headers directly and require no credentials, no
/// configuration and no live instance.
/// </para>
/// </summary>
public class HttpExtensionsTests
{
	/// <summary>
	/// Shaped like a real token so that a partial-redaction bug would be visible, but not a real one.
	/// </summary>
	private const string FakeToken = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOP";

	/// <summary>
	/// The header this client actually authenticates with. An exact-match list of the standard header
	/// names, copied from a sibling package, would render this verbatim while appearing to be fixed,
	/// so this is the single most important assertion in the file.
	/// </summary>
	[Fact]
	public void AppendRedacted_SamanageAuthorizationHeader_DoesNotLeakTheToken()
	{
		using var request = new HttpRequestMessage();
		request.Headers.TryAddWithoutValidation("X-Samanage-Authorization", $"Bearer {FakeToken}");

		var log = Render(request.Headers);

		log.Should().NotContain(FakeToken);
		log.Should().Be($"  X-Samanage-Authorization: Bearer <redacted, length {FakeToken.Length}>{Environment.NewLine}");
	}

	/// <summary>
	/// Mirrors how SolarWindsServiceDeskClient sets the header, so the test breaks if that changes.
	/// </summary>
	[Fact]
	public void AppendRedacted_HeaderSetTheWayTheClientSetsIt_IsRedacted()
	{
		using var httpClient = new HttpClient();
		httpClient.DefaultRequestHeaders.Add("X-Samanage-Authorization", "Bearer " + FakeToken);

		var log = Render(httpClient.DefaultRequestHeaders);

		log.Should().NotContain(FakeToken);
		log.Should().Contain("<redacted");
	}

	/// <summary>
	/// A standard bearer token keeps its scheme and length, and loses the credential.
	/// </summary>
	[Fact]
	public void AppendRedacted_BearerToken_KeepsTheSchemeAndLength()
	{
		using var request = new HttpRequestMessage();
		request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", FakeToken);

		var log = Render(request.Headers);

		log.Should().Be($"  Authorization: Bearer <redacted, length {FakeToken.Length}>{Environment.NewLine}");
	}

	/// <summary>
	/// Basic authentication, used by the Orion client, is redacted the same way.
	/// </summary>
	[Fact]
	public void AppendRedacted_BasicScheme_KeepsTheSchemeAndRedactsTheCredential()
	{
		using var request = new HttpRequestMessage();
		request.Headers.Authorization = new AuthenticationHeaderValue("Basic", "dXNlcjpwYXNzd29yZA==");

		var log = Render(request.Headers);

		log.Should().Contain("Basic <redacted, length 20>");
		log.Should().NotContain("dXNlcjpwYXNzd29yZA==");
	}

	/// <summary>
	/// A header added without validation keeps whatever casing the caller used, so redaction must not
	/// depend on the header name being canonically cased.
	/// </summary>
	[Theory]
	[InlineData("authorization")]
	[InlineData("AUTHORIZATION")]
	[InlineData("x-samanage-authorization")]
	[InlineData("X-SAMANAGE-AUTHORIZATION")]
	public void AppendRedacted_AuthorizationHeader_IsRedactedWhateverTheCasing(string headerName)
	{
		using var request = new HttpRequestMessage();
		request.Headers.TryAddWithoutValidation(headerName, $"Bearer {FakeToken}");

		var log = Render(request.Headers);

		log.Should().NotContain(FakeToken);
		log.Should().Contain("<redacted");
	}

	/// <summary>
	/// The other standard credential-bearing header names are redacted too.
	/// </summary>
	/// <param name="headerName">The credential-bearing header name under test.</param>
	[Theory]
	[InlineData("Proxy-Authorization")]
	[InlineData("Cookie")]
	[InlineData("X-API-Key")]
	[InlineData("Api-Key")]
	[InlineData("X-Api-Token")]
	[InlineData("X-Auth-Token")]
	public void AppendRedacted_OtherCredentialHeaders_AreRedacted(string headerName)
	{
		const string secret = "s3cr3t-value-that-must-not-be-logged";
		using var request = new HttpRequestMessage();
		request.Headers.TryAddWithoutValidation(headerName, secret);

		var log = Render(request.Headers);

		log.Should().NotContain(secret);
		log.Should().Contain("<redacted");
	}

	/// <summary>
	/// A credential with no scheme prefix has nothing safe to preserve, so all of it goes.
	/// </summary>
	[Fact]
	public void AppendRedacted_CredentialWithoutAScheme_IsRedactedEntirely()
	{
		using var request = new HttpRequestMessage();
		request.Headers.TryAddWithoutValidation("X-API-Key", "abcdef123456");

		var log = Render(request.Headers);

		log.Should().Be($"  X-API-Key: <redacted, length 12>{Environment.NewLine}");
	}

	/// <summary>
	/// A cookie value also contains a space, so treating the text before the first space as a scheme
	/// would preserve the very value being redacted. Only Authorization style headers keep a scheme.
	/// </summary>
	[Fact]
	public void AppendRedacted_CookieValueContainingASpace_IsRedactedWhole()
	{
		const string cookie = "session=abc123def456; HttpOnly";
		using var request = new HttpRequestMessage();
		request.Headers.TryAddWithoutValidation("Cookie", cookie);

		var log = Render(request.Headers);

		log.Should().Be($"  Cookie: <redacted, length {cookie.Length}>{Environment.NewLine}");
		log.Should().NotContain("session");
	}

	/// <summary>
	/// A header carrying no credential is rendered exactly as before.
	/// </summary>
	[Fact]
	public void AppendRedacted_NonSensitiveHeader_IsUnchanged()
	{
		using var request = new HttpRequestMessage();
		request.Headers.TryAddWithoutValidation("Accept", "application/vnd.samanage.v2.1+json");

		var log = Render(request.Headers);

		log.Should().Be($"  Accept: application/vnd.samanage.v2.1+json{Environment.NewLine}");
	}

	/// <summary>
	/// Redaction must be surgical: the diagnostically useful headers alongside the credential are what
	/// make a log message worth reading, so they must survive intact.
	/// </summary>
	[Fact]
	public void AppendRedacted_RedactsOnlyTheSensitiveHeader()
	{
		using var request = new HttpRequestMessage();
		request.Headers.TryAddWithoutValidation("X-Samanage-Authorization", $"Bearer {FakeToken}");
		request.Headers.TryAddWithoutValidation("Accept", "application/vnd.samanage.v2.1+json");
		request.Headers.TryAddWithoutValidation("traceparent", "00-abc123-def456-00");

		var log = Render(request.Headers);

		log.Should().NotContain(FakeToken);
		log.Should().Contain("  Accept: application/vnd.samanage.v2.1+json");
		log.Should().Contain("  traceparent: 00-abc123-def456-00");
	}

	/// <summary>
	/// Response headers go through the same helper, so Set-Cookie is covered too.
	/// </summary>
	[Fact]
	public void AppendRedacted_ResponseSetCookie_IsRedacted()
	{
		using var response = new HttpResponseMessage();
		response.Headers.TryAddWithoutValidation("Set-Cookie", "session=abc123def456; HttpOnly");

		var log = Render(response.Headers);

		log.Should().NotContain("abc123def456");
		log.Should().Contain("<redacted");
	}

	/// <summary>
	/// An empty header collection produces no output at all.
	/// </summary>
	[Fact]
	public void AppendRedacted_NoHeaders_WritesNothing()
	{
		using var request = new HttpRequestMessage();

		Render(request.Headers).Should().BeEmpty();
	}

	private static string Render(HttpHeaders headers)
	{
		var stringBuilder = new StringBuilder();
		stringBuilder.AppendRedacted(headers);
		return stringBuilder.ToString();
	}
}
