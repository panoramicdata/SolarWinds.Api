using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace SolarWinds.Api.Queries;

/// <summary>
/// The single set of JSON options shared by every Orion (SWIS) request and response.
/// </summary>
internal static class OrionJson
{
	internal static JsonSerializerOptions Options { get; } = Create();

	private static JsonSerializerOptions Create()
	{
		var options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			NumberHandling = JsonNumberHandling.AllowReadingFromString,
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
#if DEBUG
			UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
#endif
			TypeInfoResolver = new DefaultJsonTypeInfoResolver { Modifiers = { AcceptMissingRequiredMembers } }
		};

		options.MakeReadOnly();
		return options;
	}

	// SWIS omits columns that were not selected, so C# `required` must not demand them.
	private static void AcceptMissingRequiredMembers(JsonTypeInfo typeInfo)
	{
		foreach (var property in typeInfo.Properties)
		{
			property.IsRequired = false;
		}
	}
}
