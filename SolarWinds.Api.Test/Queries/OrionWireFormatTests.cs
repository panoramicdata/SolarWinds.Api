using Microsoft.Extensions.Logging.Abstractions;
using SolarWinds.Api.Exceptions;
using SolarWinds.Api.Orion;
using SolarWinds.Api.Queries;

namespace SolarWinds.Api.Test.Queries;

/// <summary>Characterization tests pinning the SWIS wire format.</summary>
public class OrionWireFormatTests
{
	private sealed class StubHandler(string responseBody, HttpStatusCode status = HttpStatusCode.OK) : HttpClientHandler
	{
		public string? RequestBody { get; private set; }

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			RequestBody = request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken);
			return new HttpResponseMessage(status) { Content = new StringContent(responseBody, Encoding.UTF8, "application/json") };
		}
	}

	private sealed class TestClient(StubHandler handler) : SolarWindsClientBase("host", 17774, handler, NullLogger.Instance);

	private const string PollerJson = """
		{"results":[{"PollerID":7,"PollerType":"N.Status.ICMP.Native","NetObject":"N:1","NetObjectType":"N","NetObjectID":"1","Enabled":"True","DisplayName":"Poller 7","Description":null,"InstanceType":"Orion.Pollers","Uri":"swis://h/Orion/Orion.Pollers/PollerID=7","InstanceSiteId":0}],"totalRows":42}
		""";

	/// <summary>Pins the current wire behaviour.</summary>
	[Fact]
	public async Task SqlQuery_SerializesWithDataMemberNames()
	{
		var handler = new StubHandler("""{"results":[]}""");
		var query = new SqlQuery { Sql = "SELECT 1", Parameters = new Dictionary<string, object> { ["p"] = 0, ["s"] = "x", ["b"] = true } };

		await new TestClient(handler).SqlQueryAsync<Poller>(query, CancellationToken.None);

		handler.RequestBody.Should().Be("""{"query":"SELECT 1","parameters":{"p":0,"s":"x","b":true}}""");
	}

	/// <summary>Markup characters in SWQL are sent literally, as before.</summary>
	[Fact]
	public async Task SqlQuery_DoesNotEscapeMarkupCharacters()
	{
		var handler = new StubHandler("""{"results":[]}""");

		await new TestClient(handler).SqlQueryAsync<Poller>(new SqlQuery { Sql = "SELECT 1 WHERE a>@p AND b<'x' & c" }, CancellationToken.None);

		handler.RequestBody.Should().Be("""{"query":"SELECT 1 WHERE a>@p AND b<'x' & c","parameters":{}}""");
	}

	/// <summary>Pins the current wire behaviour.</summary>
	[Fact]
	public async Task TypedResponse_MapsDataMemberNames()
	{
		var result = await new TestClient(new StubHandler(PollerJson)).SqlQueryAsync<Poller>(new SqlQuery { Sql = "SELECT 1" }, CancellationToken.None);

		result.TotalRows.Should().Be(42);
		var poller = result.Results.Should().ContainSingle().Subject;
		poller.PollerID.Should().Be(7);
		poller.PollerType.Should().Be("N.Status.ICMP.Native");
		poller.Enabled.Should().Be("True");
		poller.DisplayName.Should().Be("Poller 7");
		poller.InstanceType.Should().Be("Orion.Pollers");
	}

	/// <summary>Pins the current wire behaviour.</summary>
	[Fact]
	public async Task TypedResponse_NullForNonNullableStringIsAccepted()
	{
		var result = await new TestClient(new StubHandler(PollerJson)).SqlQueryAsync<Poller>(new SqlQuery { Sql = "SELECT 1" }, CancellationToken.None);

		result.Results[0].Description.Should().BeNull();
	}

	/// <summary>Pins the current wire behaviour.</summary>
	[Fact]
	public async Task TypedResponse_MissingRequiredMembersAreAccepted()
	{
		var result = await new TestClient(new StubHandler("""{"results":[{"PollerID":3}]}""")).SqlQueryAsync<Poller>(new SqlQuery { Sql = "SELECT 1" }, CancellationToken.None);

		result.Results.Should().ContainSingle().Which.PollerID.Should().Be(3);
		result.TotalRows.Should().BeNull();
	}

	/// <summary>Pins the current wire behaviour.</summary>
	[Fact]
	public async Task TypedResponse_MemberNamesAreCaseInsensitive()
	{
		var result = await new TestClient(new StubHandler("""{"RESULTS":[{"pollerid":9}],"TotalRows":1}""")).SqlQueryAsync<Poller>(new SqlQuery { Sql = "SELECT 1" }, CancellationToken.None);

		result.TotalRows.Should().Be(1);
		result.Results.Should().ContainSingle().Which.PollerID.Should().Be(9);
	}

	/// <summary>Pins the current wire behaviour.</summary>
	[Fact]
	public async Task TypedResponse_NumbersInStringsAndDatesAreConverted()
	{
		const string json = """{"results":[{"NodeID":"12","DynamicIP":true,"UnManageFrom":"2024-05-06T07:08:09.000Z","LastBoot":"2024-05-06T07:08:09","Status":1}]}""";

		var result = await new TestClient(new StubHandler(json)).SqlQueryAsync<Node>(new SqlQuery { Sql = "SELECT 1" }, CancellationToken.None);

		var node = result.Results.Should().ContainSingle().Subject;
		node.NodeID.Should().Be(12);
		node.UnManageFrom.Should().Be(new DateTime(2024, 5, 6, 7, 8, 9, DateTimeKind.Utc));
		node.UnManageFrom.Kind.Should().Be(DateTimeKind.Utc);
		node.LastBoot.Should().Be(new DateTime(2024, 5, 6, 7, 8, 9, DateTimeKind.Unspecified));
		node.LastBoot.Kind.Should().Be(DateTimeKind.Unspecified);
	}

	/// <summary>Pins the current wire behaviour.</summary>
	[Fact]
	public async Task JObjectQuery_ReturnsRawRows()
	{
		var result = await new TestClient(new StubHandler(PollerJson)).SqlJObjectQueryAsync(new SqlQuery { Sql = "SELECT 1" }, CancellationToken.None);

		var row = result.Results.Should().ContainSingle().Subject;
		((int)row["PollerID"]!).Should().Be(7);
		row.ContainsKey("Description").Should().BeTrue();
		row["Description"].Should().BeNull();
	}

	/// <summary>Unknown members throw in DEBUG builds and are ignored otherwise.</summary>
	[Fact]
	public async Task UnknownMember_IsConfigurationDependent()
	{
		const string json = """{"results":[{"PollerID":7,"Unknown":{"a":1}}]}""";
		var act = () => new TestClient(new StubHandler(json)).SqlQueryAsync<Poller>(new SqlQuery { Sql = "SELECT 1" }, CancellationToken.None);

#if DEBUG
		await act.Should().ThrowAsync<SolarWindsApiDeserializationException>();
#else
		var result = await act();
		result.Results.Should().ContainSingle().Which.PollerID.Should().Be(7);
#endif
	}

	/// <summary>Pins the current wire behaviour.</summary>
	[Fact]
	public async Task MalformedBody_ThrowsDeserializationException()
	{
		var act = () => new TestClient(new StubHandler("not json")).SqlQueryAsync<Poller>(new SqlQuery { Sql = "SELECT 1" }, CancellationToken.None);

		await act.Should().ThrowAsync<SolarWindsApiDeserializationException>();
	}

	/// <summary>Pins the current wire behaviour.</summary>
	[Fact]
	public async Task NullJsonBody_ThrowsDeserializationException()
	{
		var act = () => new TestClient(new StubHandler("null")).SqlQueryAsync<Poller>(new SqlQuery { Sql = "SELECT 1" }, CancellationToken.None);

		await act.Should().ThrowAsync<SolarWindsApiDeserializationException>();
	}

	/// <summary>Pins the current wire behaviour.</summary>
	[Fact]
	public async Task WrongTypeForMember_ThrowsDeserializationException()
	{
		var act = () => new TestClient(new StubHandler("""{"results":[{"PollerID":"abc"}]}""")).SqlQueryAsync<Poller>(new SqlQuery { Sql = "SELECT 1" }, CancellationToken.None);

		await act.Should().ThrowAsync<SolarWindsApiDeserializationException>();
	}

	/// <summary>Pins the current wire behaviour.</summary>
	[Fact]
	public async Task HttpFailure_ThrowsHttpException()
	{
		var act = () => new TestClient(new StubHandler("{}", HttpStatusCode.InternalServerError)).SqlQueryAsync<Poller>(new SqlQuery { Sql = "SELECT 1" }, CancellationToken.None);

		await act.Should().ThrowAsync<SolarWindsApiHttpException>();
	}
}
