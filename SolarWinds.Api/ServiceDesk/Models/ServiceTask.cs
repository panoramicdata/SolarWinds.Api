using System;
using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk task.
/// </summary>
public class ServiceTask : WorkItemEntity
{
	/// <summary>
	/// Gets or sets the task status.
	/// </summary>
	public required string Status { get; set; }

	/// <summary>
	/// Gets or sets the due date.
	/// </summary>
	public DateTime DueDate { get; set; }
}