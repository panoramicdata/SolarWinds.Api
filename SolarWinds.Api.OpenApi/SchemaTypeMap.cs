using System.Collections;
using System.Text.Json.Nodes;

namespace SolarWinds.Api.OpenApi;

/// <summary>
/// Maps CLR types to inline OpenAPI schemas and inspects collection element types.
/// </summary>
internal static class SchemaTypeMap
{
	/// <summary>
	/// The primitive and primitive-like types that are described in place rather than as a
	/// named component schema, mapped to their OpenAPI type and optional format.
	/// </summary>
	private static readonly Dictionary<Type, (string Type, string? Format)> InlineSchemas = new()
	{
		[typeof(string)] = ("string", null),
		[typeof(bool)] = ("boolean", null),
		[typeof(byte)] = ("integer", "int32"),
		[typeof(short)] = ("integer", "int32"),
		[typeof(int)] = ("integer", "int32"),
		[typeof(long)] = ("integer", "int64"),
		[typeof(float)] = ("number", "float"),
		[typeof(double)] = ("number", "double"),
		[typeof(decimal)] = ("number", "double"),
		[typeof(DateTime)] = ("string", "date-time"),
		[typeof(DateTimeOffset)] = ("string", "date-time"),
		[typeof(DateOnly)] = ("string", "date"),
		[typeof(TimeOnly)] = ("string", null),
		[typeof(TimeSpan)] = ("string", null),
		[typeof(Guid)] = ("string", "uuid"),
		[typeof(Uri)] = ("string", "uri")
	};

	/// <summary>
	/// Builds the in-place schema for a primitive-like type, or returns <see langword="null"/>
	/// for anything that needs a component schema of its own (an enum included).
	/// </summary>
	internal static JsonObject? TryBuildInlineSchema(Type type)
	{
		if (!InlineSchemas.TryGetValue(type, out var mapping))
		{
			return null;
		}

		var schema = new JsonObject
		{
			["type"] = mapping.Type
		};

		if (mapping.Format is not null)
		{
			schema["format"] = mapping.Format;
		}

		return schema;
	}

	internal static bool TryGetEnumerableElementType(Type type, out Type? elementType)
	{
		elementType = null;
		if (type == typeof(string) || type == typeof(byte[]))
		{
			return false;
		}

		if (type.IsArray)
		{
			elementType = type.GetElementType();
			return elementType is not null;
		}

		if (!typeof(IEnumerable).IsAssignableFrom(type))
		{
			return false;
		}

		elementType = GetSoleGenericArgument(type) ?? GetEnumerableInterfaceArgument(type);
		return elementType is not null;
	}

	/// <summary>
	/// The element type of a collection that takes exactly one type argument, such as
	/// <see cref="List{T}"/>.
	/// </summary>
	private static Type? GetSoleGenericArgument(Type type)
		=> type.IsGenericType && type.GetGenericArguments() is [var single] ? single : null;

	/// <summary>
	/// The element type taken from whichever <see cref="IEnumerable{T}"/> the type implements.
	/// </summary>
	private static Type? GetEnumerableInterfaceArgument(Type type)
		=> type.GetInterfaces()
			.FirstOrDefault(static i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			?.GetGenericArguments()[0];

	internal static bool TryGetDictionaryValueType(Type type, out Type? valueType)
	{
		valueType = null;
		var direct = type.GetInterfaces().Append(type)
			.FirstOrDefault(static i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDictionary<,>));
		if (direct is null)
		{
			return false;
		}

		var args = direct.GetGenericArguments();
		if (args[0] != typeof(string))
		{
			return false;
		}

		valueType = args[1];
		return true;
	}
}
