using System.Collections.Generic;
using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a Service Desk Configuration Item (CI).
/// </summary>
public class ConfigurationItem : DescribedEntity
{
	/// <summary>
	/// Gets or sets the CI type.
	/// </summary>
	public object? Type { get; set; }

	/// <summary>
	/// Gets or sets the list of dependent asset IDs.
	/// </summary>
	public List<int> DependentAssetIds { get; set; } = [];
}