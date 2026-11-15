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
			if (SchemaTypeMap.TryBuildInlineSchema(normalized) is { } inline)
			{
				return inline;
			}

			if (normalized == typeof(object) || normalized == typeof(JsonElement))
			{
				return new JsonObject { ["type"] = "object" };
			}

			if (SchemaTypeMap.TryGetEnumerableElementType(normalized, out var elementType))
			{
				return new JsonObject
				{
					["type"] = "array",
					["items"] = GetSchema(elementType!)
				};
			}

			if (SchemaTypeMap.TryGetDictionaryValueType(normalized, out var valueType))
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
	}
}
