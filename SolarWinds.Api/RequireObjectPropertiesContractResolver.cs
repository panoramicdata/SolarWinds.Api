using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace SolarWinds.Api
{
	/// <summary>
	/// A require object properties contract resolver
	/// </summary>
	public class RequireObjectPropertiesContractResolver : DefaultContractResolver
	{
		/// <summary>
		/// Create an object contract
		/// </summary>
		/// <param name="objectType">The object type</param>
		/// <returns></returns>
		protected override JsonObjectContract CreateObjectContract(Type objectType)
		{
			var contract = base.CreateObjectContract(objectType);
			contract.ItemRequired = Required.Always;
			return contract;
		}
	}
}