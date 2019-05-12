using System.Runtime.Serialization;

namespace SolarWinds.Api.Test
{
	/// <summary>
	/// Test config
	/// </summary>
	[DataContract]
	public class Config
	{
		/// <summary>
		/// The hostname
		/// </summary>
		[DataMember(Name = "Hostname")]
		public string Hostname { get; internal set; }

		/// <summary>
		/// The username
		/// </summary>
		[DataMember(Name = "Username")]
		public string Username { get; internal set; }

		/// <summary>
		/// The password
		/// </summary>
		[DataMember(Name = "Password")]
		public string Password { get; internal set; }

		/// <summary>
		/// Whether to ignore SSL Certificate Errors
		/// </summary>
		[DataMember(Name = "IgnoreSslCertificateErrors")]
		public bool IgnoreSslCertificateErrors { get; set; }
	}
}
