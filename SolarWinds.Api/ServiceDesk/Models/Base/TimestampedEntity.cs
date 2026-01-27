using System;

namespace SolarWinds.Api.ServiceDesk.Models.Base;

/// <summary>
/// Base class for entities with creation and update timestamps.
/// </summary>
public abstract class TimestampedEntity : BaseEntity
{
	/// <summary>
	/// Gets or sets the creation timestamp.
	/// </summary>
	public DateTime CreatedAt { get; set; }

	/// <summary>
	/// Gets or sets the update timestamp.
	/// </summary>
	public DateTime UpdatedAt { get; set; }
}
