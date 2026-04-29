namespace SolarWinds.Api.ServiceDesk.Models.Base;

/// <summary>
/// Base class for assignable work item entities (incidents, problems, changes, etc.).
/// </summary>
public abstract class WorkItemEntity : DescribedEntity
{
/// <summary>
/// Gets or sets the assignee payload.
/// </summary>
public object? Assignee { get; set; }

/// <summary>
/// Gets or sets the requester payload.
/// </summary>
public object? Requester { get; set; }
}