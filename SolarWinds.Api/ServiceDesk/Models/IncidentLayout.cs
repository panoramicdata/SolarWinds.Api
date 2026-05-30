using System.Runtime.Serialization;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Response layout options for Service Desk list/get endpoints.
/// </summary>
public enum ResponseLayout
{
	/// <summary>
	/// Retrieves the default compact payload.
	/// </summary>
	[EnumMember(Value = "short")]
	Short,

	/// <summary>
	/// Retrieves the detailed payload.
	/// </summary>
	[EnumMember(Value = "long")]
	Long,
}