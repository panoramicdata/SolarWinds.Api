using System.Text.Json.Serialization;

namespace SolarWinds.Api.Orion;

/// <summary>
/// A Node
/// </summary>
public class Node : ManagedEntity
{
	/// <summary>
	/// NodeID
	/// </summary>
	[JsonPropertyName("NodeID")]
	public int NodeID { get; set; }

	/// <summary>
	/// ObjectSubType
	/// </summary>
	[JsonPropertyName("ObjectSubType")]
	public required string ObjectSubType { get; set; }

	/// <summary>
	/// IPAddress
	/// </summary>
	[JsonPropertyName("IPAddress")]
	public required string IPAddress { get; set; }

	/// <summary>
	/// IPAddressType
	/// </summary>
	[JsonPropertyName("IPAddressType")]
	public required string IPAddressType { get; set; }

	/// <summary>
	/// DynamicIP
	/// </summary>
	[JsonPropertyName("DynamicIP")]
	public bool DynamicIP { get; set; }

	/// <summary>
	/// Caption
	/// </summary>
	[JsonPropertyName("Caption")]
	public required string Caption { get; set; }

	/// <summary>
	/// NodeDescription
	/// </summary>
	[JsonPropertyName("NodeDescription")]
	public required string NodeDescription { get; set; }

	/// <summary>
	/// DNS
	/// </summary>
	[JsonPropertyName("DNS")]
	public required string DNS { get; set; }

	/// <summary>
	/// SysName
	/// </summary>
	[JsonPropertyName("SysName")]
	public required string SysName { get; set; }

	/// <summary>
	/// SysName
	/// </summary>
	[JsonPropertyName("Vendor")]
	public required string Vendor { get; set; }

	/// <summary>
	/// SysObjectID
	/// </summary>
	[JsonPropertyName("SysObjectID")]
	public required string SysObjectID { get; set; }

	/// <summary>
	/// Location
	/// </summary>
	[JsonPropertyName("Location")]
	public required string Location { get; set; }

	/// <summary>
	/// Contact
	/// </summary>
	[JsonPropertyName("Contact")]
	public required string Contact { get; set; }

	/// <summary>
	/// VendorIcon
	/// </summary>
	[JsonPropertyName("VendorIcon")]
	public required string VendorIcon { get; set; }

	/// <summary>
	/// Icon
	/// </summary>
	[JsonPropertyName("Icon")]
	public required string Icon { get; set; }

	/// <summary>
	/// CustomStatus
	/// </summary>
	[JsonPropertyName("CustomStatus")]
	public bool CustomStatus { get; set; }

	/// <summary>
	/// IOSImage
	/// </summary>
	[JsonPropertyName("IOSImage")]
	public required string IOSImage { get; set; }

	/// <summary>
	/// IOSVersion
	/// </summary>
	[JsonPropertyName("IOSVersion")]
	public required string IOSVersion { get; set; }

	/// <summary>
	/// GroupStatus
	/// </summary>
	[JsonPropertyName("GroupStatus")]
	public required string GroupStatus { get; set; }

	/// <summary>
	/// StatusIcon
	/// </summary>
	[JsonPropertyName("StatusIcon")]
	public required string StatusIcon { get; set; }

	/// <summary>
	/// LastBoot
	/// </summary>
	[JsonPropertyName("LastBoot")]
	public DateTime LastBoot { get; set; }

	/// <summary>
	/// SystemUpTime
	/// </summary>
	[JsonPropertyName("SystemUpTime")]
	public double SystemUpTime { get; set; }

	/// <summary>
	/// SystemUpTime
	/// </summary>
	[JsonPropertyName("ResponseTime")]
	public int ResponseTime { get; set; }

	/// <summary>
	/// PercentLoss
	/// </summary>
	[JsonPropertyName("PercentLoss")]
	public double PercentLoss { get; set; }

	/// <summary>
	/// AvgResponseTime
	/// </summary>
	[JsonPropertyName("AvgResponseTime")]
	public int AvgResponseTime { get; set; }

	/// <summary>
	/// MinResponseTime
	/// </summary>
	[JsonPropertyName("MinResponseTime")]
	public int MinResponseTime { get; set; }

	/// <summary>
	/// MaxResponseTime
	/// </summary>
	[JsonPropertyName("MaxResponseTime")]
	public int MaxResponseTime { get; set; }

	///// <summary>
	///// CPU Count
	///// </summary>

	/// <summary>
	/// CPULoad
	/// </summary>
	[JsonPropertyName("CPULoad")]
	public int CPULoad { get; set; }

	/// <summary>
	/// MemoryUsed
	/// </summary>
	[JsonPropertyName("MemoryUsed")]
	public double MemoryUsed { get; set; }

	///// <summary>
	///// LoadAverage1
	///// </summary>


	///// <summary>
	///// LoadAverage5
	///// </summary>


	///// <summary>
	///// LoadAverage15
	///// </summary>


	/// <summary>
	/// MemoryAvailable
	/// </summary>
	[JsonPropertyName("MemoryAvailable")]
	public double MemoryAvailable { get; set; }

	/// <summary>
	/// MemoryAvailable
	/// </summary>
	[JsonPropertyName("PercentMemoryUsed")]
	public int PercentMemoryUsed { get; set; }

	/// <summary>
	/// PercentMemoryAvailable
	/// </summary>
	[JsonPropertyName("PercentMemoryAvailable")]
	public int PercentMemoryAvailable { get; set; }

	/// <summary>
	/// LastSync
	/// </summary>
	[JsonPropertyName("LastSync")]
	public DateTime LastSync { get; set; }

	/// <summary>
	/// LastSystemUpTimePollUtc
	/// </summary>
	[JsonPropertyName("LastSystemUpTimePollUtc")]
	public DateTime LastSystemUpTimePollUtc { get; set; }

	/// <summary>
	/// MachineType
	/// </summary>
	[JsonPropertyName("MachineType")]
	public required string MachineType { get; set; }

	/// <summary>
	/// IsServer
	/// </summary>
	[JsonPropertyName("IsServer")]
	public bool IsServer { get; set; }

	/// <summary>
	/// Severity
	/// </summary>
	[JsonPropertyName("Severity")]
	public int Severity { get; set; }

	/// <summary>
	/// UiSeverity
	/// </summary>
	[JsonPropertyName("UiSeverity")]
	public int UiSeverity { get; set; }

	/// <summary>
	/// ChildStatus
	/// </summary>
	[JsonPropertyName("ChildStatus")]
	public int ChildStatus { get; set; }

	/// <summary>
	/// Allow64BitCounters
	/// </summary>
	[JsonPropertyName("Allow64BitCounters")]
	public bool Allow64BitCounters { get; set; }

	/// <summary>
	/// AgentPort
	/// </summary>
	[JsonPropertyName("AgentPort")]
	public required string AgentPort { get; set; }

	/// <summary>
	/// TotalMemory
	/// </summary>
	[JsonPropertyName("TotalMemory")]
	public float TotalMemory { get; set; }

	/// <summary>
	/// CMTS
	/// </summary>
	[JsonPropertyName("CMTS")]
	public char CMTS { get; set; }

	/// <summary>
	/// CustomPollerLastStatisticsPoll
	/// </summary>
	[JsonPropertyName("CustomPollerLastStatisticsPoll")]
	public DateTime CustomPollerLastStatisticsPoll { get; set; }

	/// <summary>
	/// CustomPollerLastStatisticsPollSuccess
	/// </summary>
	[JsonPropertyName("CustomPollerLastStatisticsPollSuccess")]
	public DateTime CustomPollerLastStatisticsPollSuccess { get; set; }

	/// <summary>
	/// CustomPollerLastStatisticsPollSuccess
	/// </summary>
	[JsonPropertyName("SNMPVersion")]
	public short SNMPVersion { get; set; }

	/// <summary>
	/// PollInterval
	/// </summary>
	[JsonPropertyName("PollInterval")]
	public int PollInterval { get; set; }

	/// <summary>
	/// EngineID
	/// </summary>
	[JsonPropertyName("EngineID")]
	public int EngineID { get; set; }

	/// <summary>
	/// RediscoveryInterval
	/// </summary>
	[JsonPropertyName("RediscoveryInterval")]
	public int RediscoveryInterval { get; set; }

	/// <summary>
	/// NextPoll
	/// </summary>
	[JsonPropertyName("NextPoll")]
	public DateTime NextPoll { get; set; }

	/// <summary>
	/// NextRediscovery
	/// </summary>
	[JsonPropertyName("NextRediscovery")]
	public DateTime NextRediscovery { get; set; }

	/// <summary>
	/// StatCollection
	/// </summary>
	[JsonPropertyName("StatCollection")]
	public int StatCollection { get; set; }

	/// <summary>
	/// External
	/// </summary>
	[JsonPropertyName("External")]
	public bool External { get; set; }

	/// <summary>
	/// Community
	/// </summary>
	[JsonPropertyName("Community")]
	public required string Community { get; set; }

	/// <summary>
	/// RWCommunity
	/// </summary>
	[JsonPropertyName("RWCommunity")]
	public required string RWCommunity { get; set; }

	/// <summary>
	/// IP
	/// </summary>
	[JsonPropertyName("IP")]
	public required string IP { get; set; }

	/// <summary>
	/// IP_Address
	/// </summary>
	[JsonPropertyName("IP_Address")]
	public required string IP_Address { get; set; }

	/// <summary>
	/// IPAddressGUID
	/// </summary>
	[JsonPropertyName("IPAddressGUID")]
	public Guid IPAddressGUID { get; set; }

	/// <summary>
	/// IPAddressGUID
	/// </summary>
	[JsonPropertyName("NodeName")]
	public required string NodeName { get; set; }

	/// <summary>
	/// BlockUntil
	/// </summary>
	[JsonPropertyName("BlockUntil")]
	public DateTime BlockUntil { get; set; }

	/// <summary>
	/// BlockUntil
	/// </summary>
	[JsonPropertyName("BufferNoMemThisHour")]
	public double BufferNoMemThisHour { get; set; }

	/// <summary>
	/// BufferNoMemToday
	/// </summary>
	[JsonPropertyName("BufferNoMemToday")]
	public double BufferNoMemToday { get; set; }

	/// <summary>
	/// BufferSmMissThisHour
	/// </summary>
	[JsonPropertyName("BufferSmMissThisHour")]
	public double BufferSmMissThisHour { get; set; }

	/// <summary>
	/// BufferSmMissToday
	/// </summary>
	[JsonPropertyName("BufferSmMissToday")]
	public double BufferSmMissToday { get; set; }

	/// <summary>
	/// BufferMdMissThisHour
	/// </summary>
	[JsonPropertyName("BufferMdMissThisHour")]
	public double BufferMdMissThisHour { get; set; }

	/// <summary>
	/// BufferMdMissToday
	/// </summary>
	[JsonPropertyName("BufferMdMissToday")]
	public double BufferMdMissToday { get; set; }

	/// <summary>
	/// BufferBgMissThisHour
	/// </summary>
	[JsonPropertyName("BufferBgMissThisHour")]
	public double BufferBgMissThisHour { get; set; }

	/// <summary>
	/// BufferBgMissToday
	/// </summary>
	[JsonPropertyName("BufferBgMissToday")]
	public double BufferBgMissToday { get; set; }

	/// <summary>
	/// BufferLgMissThisHour
	/// </summary>
	[JsonPropertyName("BufferLgMissThisHour")]
	public double BufferLgMissThisHour { get; set; }

	/// <summary>
	/// BufferLgMissToday
	/// </summary>
	[JsonPropertyName("BufferLgMissToday")]
	public double BufferLgMissToday { get; set; }

	/// <summary>
	/// BufferHgMissThisHour
	/// </summary>
	[JsonPropertyName("BufferHgMissThisHour")]
	public double BufferHgMissThisHour { get; set; }

	/// <summary>
	/// BufferHgMissThisHour
	/// </summary>
	[JsonPropertyName("BufferHgMissToday")]
	public double BufferHgMissToday { get; set; }

	/// <summary>
	/// OrionIdPrefix
	/// </summary>
	[JsonPropertyName("OrionIdPrefix")]
	public required string OrionIdPrefix { get; set; }

	/// <summary>
	/// OrionIdColumn
	/// </summary>
	[JsonPropertyName("OrionIdColumn")]
	public required string OrionIdColumn { get; set; }

	/// <summary>
	/// SkippedPollingCycles
	/// </summary>
	[JsonPropertyName("SkippedPollingCycles")]
	public int SkippedPollingCycles { get; set; }

	/// <summary>
	/// MinutesSinceLastSync
	/// </summary>
	[JsonPropertyName("MinutesSinceLastSync")]
	public int MinutesSinceLastSync { get; set; }

	/// <summary>
	/// EntityType
	/// </summary>
	[JsonPropertyName("EntityType")]
	public required string EntityType { get; set; }

	/// <summary>
	/// DetailsUrl
	/// </summary>
	[JsonPropertyName("DetailsUrl")]
	public required string DetailsUrl { get; set; }

	/// <summary>
	/// Category
	/// </summary>
	[JsonPropertyName("Category")]
	public int Category { get; set; }

	/// <summary>
	/// IsOrionServer
	/// </summary>
	[JsonPropertyName("IsOrionServer")]
	public bool IsOrionServer { get; set; }
}
