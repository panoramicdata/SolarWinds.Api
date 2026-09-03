using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SolarWinds.Api.OpenApi;

internal static partial class Program
{
	private sealed class SchemaBuilder(JsonObject schemas)
	{
		private readonly Dictionary<Type, string> _schemaNames = new();
		private readonly HashSet<Type> _building = [];

		public JsonNode GetSchema(Type type)
		{
			var normalized = Nullable.GetUnderlyingType(type) ?? type;
			if (TryBuildInlineSchema(normalized) is { } inline)
			{
				return inline;
			}

			if (normalized == typeof(object) || normalized == typeof(JsonElement))
			{
				return new JsonObject { ["type"] = "object" };
			}

			if (TryGetEnumerableElementType(normalized, out var elementType))
			{
				return new JsonObject
				{
					["type"] = "array",
					["items"] = GetSchema(elementType!)
				};
			}

			if (TryGetDictionaryValueType(normalized, out var valueType))
			{
				return new JsonObject
				{
					["type"] = "object",
					["additionalProperties"] = GetSchema(valueType!)
				};
			}

			var schemaName = EnsureComponentSchema(normalized);
			return new JsonObject
			{
				["$ref"] = $"#/components/schemas/{schemaName}"
			};
		}

		private string EnsureComponentSchema(Type type)
		{
			if (_schemaNames.TryGetValue(type, out var existingName))
			{
				return existingName;
			}

			var name = GetUniqueSchemaName(type);
			_schemaNames[type] = name;
			if (_building.Contains(type))
			{
				return name;
			}

			_building.Add(type);
			try
			{
				schemas[name] = BuildComponentSchema(type);
			}
			finally
			{
				_building.Remove(type);
			}

			return name;
		}

		private JsonObject BuildComponentSchema(Type type)
		{
			if (type.IsEnum)
			{
				return BuildEnumSchema(type);
			}

			var props = GetPublicSchemaProperties(type).ToArray();
			var properties = new JsonObject();
			var required = new JsonArray();

			foreach (var prop in props)
			{
				if (prop.GetCustomAttribute<JsonIgnoreAttribute>() is { Condition: JsonIgnoreCondition.Always })
				{
					continue;
				}

				var propName = GetSerializedName(prop);
				properties[propName] = GetSchema(prop.PropertyType);
				if (IsRequired(prop))
				{
					required.Add(propName);
				}
			}

			var schema = new JsonObject
			{
				["type"] = "object",
				["properties"] = properties,
				["additionalProperties"] = false
			};

			if (required.Count > 0)
			{
				schema["required"] = required;
			}

			return schema;
		}

		private static JsonObject BuildEnumSchema(Type type)
		{
			var values = Enum.GetNames(type)
				.Select(name => type.GetField(name, BindingFlags.Public | BindingFlags.Static))
				.Where(static f => f is not null)
				.Select(static f => f!.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? f!.Name)
				.OrderBy(static v => v, StringComparer.Ordinal)
				.ToArray();

			var enumValues = new JsonArray();
			foreach (var value in values)
			{
				enumValues.Add(value);
			}

			return new JsonObject
			{
				["type"] = "string",
				["enum"] = enumValues
			};
		}

		private string GetUniqueSchemaName(Type type)
		{
			var baseName = type.Name;
			var tickIndex = baseName.IndexOf('`');
			if (tickIndex >= 0)
			{
				baseName = baseName[..tickIndex];
			}

			if (type.IsGenericType)
			{
				var args = string.Join("", type.GetGenericArguments().Select(static a => a.Name));
				baseName += args;
			}

			var name = baseName;
			var suffix = 2;
			while (schemas.ContainsKey(name))
			{
				name = baseName + suffix;
				suffix++;
			}

			return name;
		}

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
		private static JsonObject? TryBuildInlineSchema(Type type)
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

		private static bool TryGetEnumerableElementType(Type type, out Type? elementType)
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

		private static bool TryGetDictionaryValueType(Type type, out Type? valueType)
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
}
