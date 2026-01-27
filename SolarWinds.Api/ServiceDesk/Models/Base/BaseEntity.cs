namespace SolarWinds.Api.ServiceDesk.Models.Base;

/// <summary>
/// Base class for all Service Desk entities.
/// </summary>
public abstract class BaseEntity
{
	/// <summary>
	/// Gets or sets the entity ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets custom field values.
	/// </summary>
	public object? CustomFieldsValues { get; set; }
}
