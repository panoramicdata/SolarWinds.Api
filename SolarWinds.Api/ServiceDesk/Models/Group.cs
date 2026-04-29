using System.Collections.Generic;
using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk group.
/// </summary>
public class Group : NamedEntity
{
	/// <summary>
	/// Gets or sets whether the group is disabled.
	/// </summary>
	public bool Disabled { get; set; }

	/// <summary>
	/// Gets or sets whether the group is a user group.
	/// </summary>
	public bool IsUser { get; set; }

	/// <summary>
	/// Gets or sets the group avatar URL.
	/// </summary>
	public object? Avatar { get; set; }

	/// <summary>
	/// Gets or sets the reports to ID.
	/// </summary>
	public int ReportsTo { get; set; }

	/// <summary>
	/// Gets or sets the group type.
	/// </summary>
	public string Type { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets whether to send notifications to the group.
	/// </summary>
	public bool SendNotifications { get; set; }

	/// <summary>
	/// Gets or sets the group memberships.
	/// </summary>
	public List<Membership> Memberships { get; set; } = [];
}

/// <summary>
/// Represents a group membership.
/// </summary>
public class Membership : BaseEntity
{
	/// <summary>
	/// Gets or sets the user ID.
	/// </summary>
	public int UserId { get; set; }

	/// <summary>
	/// Gets or sets the group ID.
	/// </summary>
	public int GroupId { get; set; }
}