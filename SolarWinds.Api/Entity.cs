using System.Linq;
using System.Runtime.Serialization;

namespace SolarWinds.Api;

/// <summary>
/// An Entity
/// </summary>
[DataContract]
public abstract class Entity
{
	/// <summary>
	/// The Display Name
	/// </summary>
	[DataMember(Name = "DisplayName")]
	public string DisplayName { get; set; }

	/// <summary>
	/// The Description
	/// </summary>
	[DataMember(Name = "Description")]
	public string Description { get; set; }

	/// <summary>
	/// The InstanceType
	/// </summary>
	[DataMember(Name = "InstanceType")]
	public string InstanceType { get; set; }

	/// <summary>
	/// The URI
	/// </summary>
	[DataMember(Name = "Uri")]
	public string Uri { get; set; }

	/// <summary>
	/// The InstanceSiteId
	/// </summary>
	[DataMember(Name = "InstanceSiteId")]
	public int InstanceSiteId { get; set; }

	public static string GetTableFullName<T>() where T : Entity
	{
		// Try to get the Table attribute
		var tableName = (typeof(T).GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault() as TableAttribute)?.TableName;
		if (tableName != null)
		{
			return tableName;
		}
		// Failed.  Guess.

		var singular = typeof(T).FullName.Replace("SolarWinds.Api.", "");
		return singular.EndsWith("y")
			? singular
			: singular + "s";
	}
}