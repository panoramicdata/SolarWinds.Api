namespace SolarWinds.Api;

/// <summary>
/// Base type for Orion entities returned by SWIS queries.
/// </summary>
public abstract class Entity
{
	/// <summary>
	/// Gets or sets the user-facing display name.
	/// </summary>
	public string DisplayName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the entity description.
	/// </summary>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the Orion instance type name.
	/// </summary>
	public string InstanceType { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the Orion URI that identifies this entity.
	/// </summary>
	public string Uri { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the site identifier associated with this entity instance.
	/// </summary>
	public int InstanceSiteId { get; set; }

	/// <summary>
	/// Resolves the SWIS table name for the entity type.
	/// </summary>
	/// <typeparam name="T">Concrete entity type to resolve.</typeparam>
	/// <returns>The fully qualified SWIS table name.</returns>
	public static string GetTableFullName<T>() where T : Entity
	{
		// Try to get the Table attribute
		var tableName = (typeof(T).GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault() as TableAttribute)?.TableName;
		if (tableName != null)
		{
			return tableName;
		}
		// Failed.  Guess.

		var singular = typeof(T).FullName?.Replace("SolarWinds.Api.", "", StringComparison.Ordinal) ?? string.Empty;
		return singular.EndsWith('y')
			? singular
			: singular + "s";
	}
}