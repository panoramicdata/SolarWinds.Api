using System.Text.Json.Serialization;

namespace SolarWinds.Api.Orion;

	/// <summary>
	/// A Credential
	/// </summary>
	public class Credential : Entity
	{
		/// <summary>
		/// Id
		/// </summary>
		[JsonPropertyName("Id")]
		public int Id { get; set; }

		/// <summary>
		/// Name
		/// </summary>
		[JsonPropertyName("Name")]
		public required string Name { get; set; }

		/// <summary>
		/// CredentialType
		/// </summary>
		[JsonPropertyName("CredentialType")]
		public required string CredentialType { get; set; }

		/// <summary>
		/// CredentialOwner
		/// </summary>
		[JsonPropertyName("CredentialOwner")]
		public required string CredentialOwner { get; set; }
	}
