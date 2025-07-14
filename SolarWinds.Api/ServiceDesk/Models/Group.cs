using System;
using System.Collections.Generic;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk group.
/// </summary>
public class Group
{
	/// <summary>
	/// Gets or sets the group ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the group name.
	/// </summary>
	public string Name { get; set; }

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
	public string Avatar { get; set; }

	/// <summary>
	/// Gets or sets the reports to ID.
	/// </summary>
	public int ReportsTo { get; set; }

	/// <summary>
	/// Gets or sets the group type.
	/// </summary>
	public string Type { get; set; }

	/// <summary>
	/// Gets or sets whether to send notifications to the group.
	/// </summary>
	public bool SendNotifications { get; set; }

	/// <summary>
	/// Gets or sets the group memberships.
	/// </summary>
	public List<Membership> Memberships { get; set; }

	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// Represents a group membership.
/// </summary>
public class Membership
{
	/// <summary>
	/// Gets or sets the membership ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the user ID.
	/// </summary>
	public int UserId { get; set; }

	/// <summary>
	/// Gets or sets the group ID.
	/// </summary>
	public int GroupId { get; set; }
}