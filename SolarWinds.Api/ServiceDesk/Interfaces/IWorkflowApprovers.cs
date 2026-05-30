using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;

/// <summary>
/// Interface for the Service Desk Workflow Approvers API.
/// </summary>
public interface IWorkflowApprovers
{
	/// <summary>
	/// Gets workflow approver details for one or more approver IDs.
	/// </summary>
	/// <param name="ids">The approver IDs to retrieve.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of workflow approvers.</returns>
	[Get("/workflow_approvers.json")]
	public Task<List<WorkflowApprover>> GetAsync([Query(CollectionFormat.Multi), AliasAs("ids[]")] IEnumerable<int> ids, CancellationToken cancellationToken);
}
