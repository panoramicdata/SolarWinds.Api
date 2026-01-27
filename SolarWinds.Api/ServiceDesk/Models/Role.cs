using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk role.
/// </summary>
public class Role : DescribedEntity
{
	/// <summary>
	/// Gets or sets whether the role has portal access.
	/// </summary>
	public bool Portal { get; set; }

	/// <summary>
	/// Gets or sets whether the role shows "My Tasks".
	/// </summary>
	public bool ShowMyTasks { get; set; }
}