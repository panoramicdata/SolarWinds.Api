namespace SolarWinds.Api.ServiceDesk.Models.Base;

/// <summary>
/// Base class for entities with a name, description, and timestamps.
/// </summary>
public abstract class DescribedEntity : NamedEntity
{
	/// <summary>
	/// Gets or sets the entity description.
	/// </summary>
	public string Description { get; set; } = string.Empty;
}
