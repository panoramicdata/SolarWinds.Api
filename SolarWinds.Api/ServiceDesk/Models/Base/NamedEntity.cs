namespace SolarWinds.Api.ServiceDesk.Models.Base;

/// <summary>
/// Base class for entities with a name and timestamps.
/// </summary>
public abstract class NamedEntity : TimestampedEntity
{
	/// <summary>
	/// Gets or sets the entity name.
	/// </summary>
	public string Name { get; set; } = string.Empty;
}
