using System.Runtime.Serialization;

namespace SolarWinds.Api.Orion
{
	/// <summary>
	/// A Custom Property
	/// </summary>
	[DataContract]
	[Table("Orion.CustomProperty")]
	public class CustomProperty : Entity
	{
		/// <summary>
		/// The table
		/// </summary>
		[DataMember(Name = "Table")]
		public string Table { get; set; }

		/// <summary>
		/// The Field
		/// </summary>
		[DataMember(Name = "Field")]
		public string Field { get; set; }

		/// <summary>
		/// The DataType
		/// </summary>
		[DataMember(Name = "DataType")]
		public string DataType { get; set; }

		/// <summary>
		/// The max length
		/// </summary>
		[DataMember(Name = "MaxLength")]
		public int MaxLength { get; set; }

		/// <summary>
		/// The StorageMethod
		/// </summary>
		[DataMember(Name = "StorageMethod")]
		public string StorageMethod { get; set; }

		/// <summary>
		/// The TargetEntity
		/// </summary>
		[DataMember(Name = "TargetEntity")]
		public string TargetEntity { get; set; }

		/// <summary>
		/// Whether this is mandatory
		/// </summary>
		[DataMember(Name = "Mandatory")]
		public bool Mandatory { get; set; }

		/// <summary>
		/// The Default
		/// </summary>
		[DataMember(Name = "Default")]
		public string Default { get; set; }
	}
}
