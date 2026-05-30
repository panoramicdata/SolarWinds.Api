namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents the workflow information for an incident.
/// </summary>
public class IncidentWorkflow
{
	/// <summary>
	/// Gets or sets the workflow ID.
	/// </summary>
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	/// <summary>
	/// Gets or sets the workflow name.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets the workflow steps/nodes.
	/// </summary>
	[JsonPropertyName("nodes")]
	public List<WorkflowNode> Nodes { get; set; } = [];

	/// <summary>
	/// Gets or sets whether the workflow is active.
	/// </summary>
	[JsonPropertyName("active")]
	public bool Active { get; set; }

	/// <summary>
	/// Gets or sets additional raw workflow data.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

/// <summary>
/// Represents a single node/step in a workflow.
/// </summary>
public class WorkflowNode
{
	/// <summary>
	/// Gets or sets the node ID.
	/// </summary>
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	/// <summary>
	/// Gets or sets the node name.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets the node type.
	/// </summary>
	[JsonPropertyName("node_type")]
	public string? NodeType { get; set; }

	/// <summary>
	/// Gets or sets the node status.
	/// </summary>
	[JsonPropertyName("status")]
	public string? Status { get; set; }
}
