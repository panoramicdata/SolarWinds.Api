using System.Runtime.Serialization;

namespace SolarWinds.Api.Orion;

	/// <summary>
	/// A Credential
	/// </summary>
	[DataContract]
	public class Credential : Entity
	{
		/// <summary>
		/// Id
		/// </summary>
		[DataMember(Name = "Id")]
		public int Id { get; set; }

		/// <summary>
		/// Name
		/// </summary>
		[DataMember(Name = "Name")]
		public required string Name { get; set; }

		/// <summary>
		/// CredentialType
		/// </summary>
		[DataMember(Name = "CredentialType")]
		public required string CredentialType { get; set; }

		/// <summary>
		/// CredentialOwner
		/// </summary>
		[DataMember(Name = "CredentialOwner")]
		public required string CredentialOwner { get; set; }
	}
