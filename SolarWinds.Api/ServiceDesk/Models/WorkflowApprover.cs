using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a workflow approver.
/// </summary>
public class WorkflowApprover : BaseEntity
{
	/// <summary>
	/// Gets or sets the approver's display name.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the approver's email address.
	/// </summary>
	[JsonPropertyName("email")]
	public string? Email { get; set; }

	/// <summary>
	/// Gets or sets the approver type (for example "user" or "group").
	/// </summary>
	[JsonPropertyName("type")]
	public string? Type { get; set; }

	/// <summary>
	/// Gets or sets the avatar URL.
	/// </summary>
	[JsonPropertyName("avatar")]
	public string? Avatar { get; set; }
}
