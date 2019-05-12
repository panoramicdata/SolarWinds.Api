using System;
using System.Runtime.Serialization;

namespace SolarWinds.Api.Orion
{
	/// <summary>
	/// A Node
	/// </summary>
	[DataContract]
	public class Node : ManagedEntity
	{
		/// <summary>
		/// NodeID
		/// </summary>
		[DataMember(Name = "NodeID")]
		public int NodeID { get; set; }

		/// <summary>
		/// ObjectSubType
		/// </summary>
		[DataMember(Name = "ObjectSubType")]
		public string ObjectSubType { get; set; }

		/// <summary>
		/// IPAddress
		/// </summary>
		[DataMember(Name = "IPAddress")]
		public string IPAddress { get; set; }

		/// <summary>
		/// IPAddressType
		/// </summary>
		[DataMember(Name = "IPAddressType")]
		public string IPAddressType { get; set; }

		/// <summary>
		/// DynamicIP
		/// </summary>
		[DataMember(Name = "DynamicIP")]
		public bool DynamicIP { get; set; }

		/// <summary>
		/// Caption
		/// </summary>
		[DataMember(Name = "Caption")]
		public string Caption { get; set; }

		/// <summary>
		/// NodeDescription
		/// </summary>
		[DataMember(Name = "NodeDescription")]
		public string NodeDescription { get; set; }

		/// <summary>
		/// DNS
		/// </summary>
		[DataMember(Name = "DNS")]
		public string DNS { get; set; }

		/// <summary>
		/// SysName
		/// </summary>
		[DataMember(Name = "SysName")]
		public string SysName { get; set; }

		/// <summary>
		/// SysName
		/// </summary>
		[DataMember(Name = "Vendor")]
		public string Vendor { get; set; }

		/// <summary>
		/// SysObjectID
		/// </summary>
		[DataMember(Name = "SysObjectID")]
		public string SysObjectID { get; set; }

		/// <summary>
		/// Location
		/// </summary>
		[DataMember(Name = "Location")]
		public string Location { get; set; }

		/// <summary>
		/// Contact
		/// </summary>
		[DataMember(Name = "Contact")]
		public string Contact { get; set; }

		/// <summary>
		/// VendorIcon
		/// </summary>
		[DataMember(Name = "VendorIcon")]
		public string VendorIcon { get; set; }

		/// <summary>
		/// Icon
		/// </summary>
		[DataMember(Name = "Icon")]
		public string Icon { get; set; }

		/// <summary>
		/// CustomStatus
		/// </summary>
		[DataMember(Name = "CustomStatus")]
		public bool CustomStatus { get; set; }

		/// <summary>
		/// IOSImage
		/// </summary>
		[DataMember(Name = "IOSImage")]
		public string IOSImage { get; set; }

		/// <summary>
		/// IOSVersion
		/// </summary>
		[DataMember(Name = "IOSVersion")]
		public string IOSVersion { get; set; }

		/// <summary>
		/// GroupStatus
		/// </summary>
		[DataMember(Name = "GroupStatus")]
		public string GroupStatus { get; set; }

		/// <summary>
		/// StatusIcon
		/// </summary>
		[DataMember(Name = "StatusIcon")]
		public string StatusIcon { get; set; }

		/// <summary>
		/// LastBoot
		/// </summary>
		[DataMember(Name = "LastBoot")]
		public DateTime LastBoot { get; set; }

		/// <summary>
		/// SystemUpTime
		/// </summary>
		[DataMember(Name = "SystemUpTime")]
		public double SystemUpTime { get; set; }

		/// <summary>
		/// SystemUpTime
		/// </summary>
		[DataMember(Name = "ResponseTime")]
		public int ResponseTime { get; set; }

		/// <summary>
		/// PercentLoss
		/// </summary>
		[DataMember(Name = "PercentLoss")]
		public double PercentLoss { get; set; }

		/// <summary>
		/// AvgResponseTime
		/// </summary>
		[DataMember(Name = "AvgResponseTime")]
		public int AvgResponseTime { get; set; }

		/// <summary>
		/// MinResponseTime
		/// </summary>
		[DataMember(Name = "MinResponseTime")]
		public int MinResponseTime { get; set; }

		/// <summary>
		/// MaxResponseTime
		/// </summary>
		[DataMember(Name = "MaxResponseTime")]
		public int MaxResponseTime { get; set; }

		///// <summary>
		///// CPU Count
		///// </summary>
		//[DataMember(Name = "CPUCount")]
		//public short CPUCount { get; set; }

		/// <summary>
		/// CPULoad
		/// </summary>
		[DataMember(Name = "CPULoad")]
		public int CPULoad { get; set; }

		/// <summary>
		/// MemoryUsed
		/// </summary>
		[DataMember(Name = "MemoryUsed")]
		public double MemoryUsed { get; set; }

		///// <summary>
		///// LoadAverage1
		///// </summary>
		//[DataMember(Name = "LoadAverage1")]
		//public double LoadAverage1 { get; set; }

		///// <summary>
		///// LoadAverage5
		///// </summary>
		//[DataMember(Name = "LoadAverage5")]
		//public double LoadAverage5 { get; set; }

		///// <summary>
		///// LoadAverage15
		///// </summary>
		//[DataMember(Name = "LoadAverage15")]
		//public double LoadAverage15 { get; set; }

		/// <summary>
		/// MemoryAvailable
		/// </summary>
		[DataMember(Name = "MemoryAvailable")]
		public double MemoryAvailable { get; set; }

		/// <summary>
		/// MemoryAvailable
		/// </summary>
		[DataMember(Name = "PercentMemoryUsed")]
		public int PercentMemoryUsed { get; set; }

		/// <summary>
		/// PercentMemoryAvailable
		/// </summary>
		[DataMember(Name = "PercentMemoryAvailable")]
		public int PercentMemoryAvailable { get; set; }

		/// <summary>
		/// LastSync
		/// </summary>
		[DataMember(Name = "LastSync")]
		public DateTime LastSync { get; set; }

		/// <summary>
		/// LastSystemUpTimePollUtc
		/// </summary>
		[DataMember(Name = "LastSystemUpTimePollUtc")]
		public DateTime LastSystemUpTimePollUtc { get; set; }

		/// <summary>
		/// MachineType
		/// </summary>
		[DataMember(Name = "MachineType")]
		public string MachineType { get; set; }

		/// <summary>
		/// IsServer
		/// </summary>
		[DataMember(Name = "IsServer")]
		public bool IsServer { get; set; }

		/// <summary>
		/// Severity
		/// </summary>
		[DataMember(Name = "Severity")]
		public int Severity { get; set; }

		/// <summary>
		/// UiSeverity
		/// </summary>
		[DataMember(Name = "UiSeverity")]
		public int UiSeverity { get; set; }

		/// <summary>
		/// ChildStatus
		/// </summary>
		[DataMember(Name = "ChildStatus")]
		public int ChildStatus { get; set; }

		/// <summary>
		/// Allow64BitCounters
		/// </summary>
		[DataMember(Name = "Allow64BitCounters")]
		public bool Allow64BitCounters { get; set; }

		/// <summary>
		/// AgentPort
		/// </summary>
		[DataMember(Name = "AgentPort")]
		public string AgentPort { get; set; }

		/// <summary>
		/// TotalMemory
		/// </summary>
		[DataMember(Name = "TotalMemory")]
		public float TotalMemory { get; set; }

		/// <summary>
		/// CMTS
		/// </summary>
		[DataMember(Name = "CMTS")]
		public char CMTS { get; set; }

		/// <summary>
		/// CustomPollerLastStatisticsPoll
		/// </summary>
		[DataMember(Name = "CustomPollerLastStatisticsPoll")]
		public DateTime CustomPollerLastStatisticsPoll { get; set; }

		/// <summary>
		/// CustomPollerLastStatisticsPollSuccess
		/// </summary>
		[DataMember(Name = "CustomPollerLastStatisticsPollSuccess")]
		public DateTime CustomPollerLastStatisticsPollSuccess { get; set; }

		/// <summary>
		/// CustomPollerLastStatisticsPollSuccess
		/// </summary>
		[DataMember(Name = "SNMPVersion")]
		public short SNMPVersion { get; set; }

		/// <summary>
		/// PollInterval
		/// </summary>
		[DataMember(Name = "PollInterval")]
		public int PollInterval { get; set; }

		/// <summary>
		/// EngineID
		/// </summary>
		[DataMember(Name = "EngineID")]
		public int EngineID { get; set; }

		/// <summary>
		/// RediscoveryInterval
		/// </summary>
		[DataMember(Name = "RediscoveryInterval")]
		public int RediscoveryInterval { get; set; }

		/// <summary>
		/// NextPoll
		/// </summary>
		[DataMember(Name = "NextPoll")]
		public DateTime NextPoll { get; set; }

		/// <summary>
		/// NextRediscovery
		/// </summary>
		[DataMember(Name = "NextRediscovery")]
		public DateTime NextRediscovery { get; set; }

		/// <summary>
		/// StatCollection
		/// </summary>
		[DataMember(Name = "StatCollection")]
		public int StatCollection { get; set; }

		/// <summary>
		/// External
		/// </summary>
		[DataMember(Name = "External")]
		public bool External { get; set; }

		/// <summary>
		/// Community
		/// </summary>
		[DataMember(Name = "Community")]
		public string Community { get; set; }

		/// <summary>
		/// RWCommunity
		/// </summary>
		[DataMember(Name = "RWCommunity")]
		public string RWCommunity { get; set; }

		/// <summary>
		/// IP
		/// </summary>
		[DataMember(Name = "IP")]
		public string IP { get; set; }

		/// <summary>
		/// IP_Address
		/// </summary>
		[DataMember(Name = "IP_Address")]
		public string IP_Address { get; set; }

		/// <summary>
		/// IPAddressGUID
		/// </summary>
		[DataMember(Name = "IPAddressGUID")]
		public Guid IPAddressGUID { get; set; }

		/// <summary>
		/// IPAddressGUID
		/// </summary>
		[DataMember(Name = "NodeName")]
		public string NodeName { get; set; }

		/// <summary>
		/// BlockUntil
		/// </summary>
		[DataMember(Name = "BlockUntil")]
		public DateTime BlockUntil { get; set; }

		/// <summary>
		/// BlockUntil
		/// </summary>
		[DataMember(Name = "BufferNoMemThisHour")]
		public double BufferNoMemThisHour { get; set; }

		/// <summary>
		/// BufferNoMemToday
		/// </summary>
		[DataMember(Name = "BufferNoMemToday")]
		public double BufferNoMemToday { get; set; }

		/// <summary>
		/// BufferSmMissThisHour
		/// </summary>
		[DataMember(Name = "BufferSmMissThisHour")]
		public double BufferSmMissThisHour { get; set; }

		/// <summary>
		/// BufferSmMissToday
		/// </summary>
		[DataMember(Name = "BufferSmMissToday")]
		public double BufferSmMissToday { get; set; }

		/// <summary>
		/// BufferMdMissThisHour
		/// </summary>
		[DataMember(Name = "BufferMdMissThisHour")]
		public double BufferMdMissThisHour { get; set; }

		/// <summary>
		/// BufferMdMissToday
		/// </summary>
		[DataMember(Name = "BufferMdMissToday")]
		public double BufferMdMissToday { get; set; }

		/// <summary>
		/// BufferBgMissThisHour
		/// </summary>
		[DataMember(Name = "BufferBgMissThisHour")]
		public double BufferBgMissThisHour { get; set; }

		/// <summary>
		/// BufferBgMissToday
		/// </summary>
		[DataMember(Name = "BufferBgMissToday")]
		public double BufferBgMissToday { get; set; }

		/// <summary>
		/// BufferLgMissThisHour
		/// </summary>
		[DataMember(Name = "BufferLgMissThisHour")]
		public double BufferLgMissThisHour { get; set; }

		/// <summary>
		/// BufferLgMissToday
		/// </summary>
		[DataMember(Name = "BufferLgMissToday")]
		public double BufferLgMissToday { get; set; }

		/// <summary>
		/// BufferHgMissThisHour
		/// </summary>
		[DataMember(Name = "BufferHgMissThisHour")]
		public double BufferHgMissThisHour { get; set; }

		/// <summary>
		/// BufferHgMissThisHour
		/// </summary>
		[DataMember(Name = "BufferHgMissToday")]
		public double BufferHgMissToday { get; set; }

		/// <summary>
		/// OrionIdPrefix
		/// </summary>
		[DataMember(Name = "OrionIdPrefix")]
		public string OrionIdPrefix { get; set; }

		/// <summary>
		/// OrionIdColumn
		/// </summary>
		[DataMember(Name = "OrionIdColumn")]
		public string OrionIdColumn { get; set; }

		/// <summary>
		/// SkippedPollingCycles
		/// </summary>
		[DataMember(Name = "SkippedPollingCycles")]
		public int SkippedPollingCycles { get; set; }

		/// <summary>
		/// MinutesSinceLastSync
		/// </summary>
		[DataMember(Name = "MinutesSinceLastSync")]
		public int MinutesSinceLastSync { get; set; }

		/// <summary>
		/// EntityType
		/// </summary>
		[DataMember(Name = "EntityType")]
		public string EntityType { get; set; }

		/// <summary>
		/// DetailsUrl
		/// </summary>
		[DataMember(Name = "DetailsUrl")]
		public string DetailsUrl { get; set; }

		/// <summary>
		/// Category
		/// </summary>
		[DataMember(Name = "Category")]
		public int Category { get; set; }

		/// <summary>
		/// IsOrionServer
		/// </summary>
		[DataMember(Name = "IsOrionServer")]
		public bool IsOrionServer { get; set; }
	}
}
