using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using SolarWinds.Api.Orion;
using SolarWinds.Api.Queries;

namespace SolarWinds.Api.Test.Queries;

/// <summary>Proves the serialised property names of every Orion model match the DataMember names of the Newtonsoft era.</summary>
public class OrionPropertyNameTests
{
	// Captured by reflection over [DataMember(Name)] on the last Newtonsoft-era build: type|sorted names.
	private const string NewtonsoftEraNames = """
		SolarWinds.Api.Entity|Description,DisplayName,InstanceSiteId,InstanceType,Uri
		SolarWinds.Api.Orion.Credential|CredentialOwner,CredentialType,Description,DisplayName,Id,InstanceSiteId,InstanceType,Name,Uri
		SolarWinds.Api.Orion.CustomProperty|DataType,Default,Description,DisplayName,Field,InstanceSiteId,InstanceType,Mandatory,MaxLength,StorageMethod,Table,TargetEntity,Uri
		SolarWinds.Api.Orion.CustomPropertyUsage|Description,DisplayName,InstanceSiteId,InstanceType,Uri
		SolarWinds.Api.Orion.CustomPropertyValue|Description,DisplayName,Field,InstanceSiteId,InstanceType,Table,Uri,Value
		SolarWinds.Api.Orion.ManagedEntity|AncestorDetailsUrls,AncestorDisplayNames,Description,DisplayName,Image,InstanceSiteId,InstanceType,Status,StatusDescription,StatusIconHint,StatusLED,UnManageFrom,UnManageUntil,UnManaged,Uri
		SolarWinds.Api.Orion.Node|AgentPort,Allow64BitCounters,AncestorDetailsUrls,AncestorDisplayNames,AvgResponseTime,BlockUntil,BufferBgMissThisHour,BufferBgMissToday,BufferHgMissThisHour,BufferHgMissToday,BufferLgMissThisHour,BufferLgMissToday,BufferMdMissThisHour,BufferMdMissToday,BufferNoMemThisHour,BufferNoMemToday,BufferSmMissThisHour,BufferSmMissToday,CMTS,CPULoad,Caption,Category,ChildStatus,Community,Contact,CustomPollerLastStatisticsPoll,CustomPollerLastStatisticsPollSuccess,CustomStatus,DNS,Description,DetailsUrl,DisplayName,DynamicIP,EngineID,EntityType,External,GroupStatus,IOSImage,IOSVersion,IP,IPAddress,IPAddressGUID,IPAddressType,IP_Address,Icon,Image,InstanceSiteId,InstanceType,IsOrionServer,IsServer,LastBoot,LastSync,LastSystemUpTimePollUtc,Location,MachineType,MaxResponseTime,MemoryAvailable,MemoryUsed,MinResponseTime,MinutesSinceLastSync,NextPoll,NextRediscovery,NodeDescription,NodeID,NodeName,ObjectSubType,OrionIdColumn,OrionIdPrefix,PercentLoss,PercentMemoryAvailable,PercentMemoryUsed,PollInterval,RWCommunity,RediscoveryInterval,ResponseTime,SNMPVersion,Severity,SkippedPollingCycles,StatCollection,Status,StatusDescription,StatusIcon,StatusIconHint,StatusLED,SysName,SysObjectID,SystemUpTime,TotalMemory,UiSeverity,UnManageFrom,UnManageUntil,UnManaged,Uri,Vendor,VendorIcon
		SolarWinds.Api.Orion.NodesCustomProperty|Description,DisplayName,InstanceSiteId,InstanceType,NodeID,Uri,cp_dvcOwner,cp_dvcTypeTier
		SolarWinds.Api.Orion.Poller|Description,DisplayName,Enabled,InstanceSiteId,InstanceType,NetObject,NetObjectID,NetObjectType,PollerID,PollerType,Uri
		SolarWinds.Api.Orion.Service|Description,DisplayName,InstanceSiteId,InstanceType,Memory,ServiceName,Uri
		SolarWinds.Api.Queries.QueryResponse`1|results,totalRows
		SolarWinds.Api.Queries.SqlQuery|parameters,query
		SolarWinds.Api.Reference|
		SolarWinds.Api.SitesReferencesSystemEntity|
		""";

	private static Dictionary<string, string> Expected() => NewtonsoftEraNames
		.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
		.Select(line => line.Split('|'))
		.ToDictionary(parts => parts[0], parts => parts[1]);

	/// <summary>Gets one row per model type: its full name and the expected names.</summary>
	public static TheoryData<string, string> Models()
	{
		var data = new TheoryData<string, string>();
		foreach (var (typeName, names) in Expected())
		{
			data.Add(typeName, names);
		}

		return data;
	}

	/// <summary>Each model serialises exactly the names it did under Newtonsoft.</summary>
	[Theory]
	[MemberData(nameof(Models))]
	public void SerialisedPropertyNames_MatchNewtonsoftEra(string typeName, string expectedNames)
	{
		var type = typeof(Entity).Assembly.GetType(typeName, throwOnError: true)!;
		if (type.IsGenericTypeDefinition)
		{
			type = type.MakeGenericType(typeof(Poller));
		}

		var typeInfo = OrionJson.Options.GetTypeInfo(type);

		var actual = typeInfo.Properties.Select(p => p.Name).Order(StringComparer.Ordinal);
		string.Join(",", actual).Should().Be(expectedNames);
	}

	/// <summary>No Orion entity or query model was left out of the table.</summary>
	[Fact]
	public void EveryOrionModelIsCovered()
	{
		var entityTypes = typeof(Entity).Assembly.GetTypes()
			.Where(t => t.IsAssignableTo(typeof(Entity)) && !t.IsAbstract && t.Namespace?.Contains("ServiceDesk", StringComparison.Ordinal) != true)
			.Select(t => t.FullName!);

		Expected().Keys.Should().Contain(entityTypes)
			.And.Contain(typeof(SqlQuery).FullName!)
			.And.Contain(typeof(QueryResponse<>).FullName!);
	}
}