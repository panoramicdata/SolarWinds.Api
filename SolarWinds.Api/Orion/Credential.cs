using System.Runtime.Serialization;

namespace SolarWinds.Api.Orion
{

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
		public string Name { get; set; }

		/// <summary>
		/// CredentialType
		/// </summary>
		[DataMember(Name = "CredentialType")]
		public string CredentialType { get; set; }

		/// <summary>
		/// CredentialOwner
		/// </summary>
		[DataMember(Name = "CredentialOwner")]
		public string CredentialOwner { get; set; }
	}
}
