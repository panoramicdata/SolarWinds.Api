namespace SolarWinds.Api.ServiceDesk.Models.Base;

/// <summary>
/// Base class for assignable work item entities (incidents, problems, changes, etc.).
/// </summary>
public abstract class WorkItemEntity : DescribedEntity
{
	/// <summary>
	/// Gets or sets the assignee's email.
	/// </summary>
	public required string Assignee { get; set; }

	/// <summary>
	/// Gets or sets the requester's email.
	/// </summary>
	public required string Requester { get; set; }
}
