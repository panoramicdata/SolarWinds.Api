using System.Collections;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Refit;

namespace SolarWinds.Api.OpenApi;

internal static partial class Program
{
	private static readonly JsonSerializerOptions JsonOptions = new()
	{
		WriteIndented = true
	};

	private static readonly NullabilityInfoContext NullabilityContext = new();

	public static int Main(string[] args)
	{
		try
		{
			var outputPath = ResolveOutputPath(args);
			var document = BuildOpenApiDocument();
			Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
			File.WriteAllText(outputPath, document.ToJsonString(JsonOptions));
			Console.WriteLine($"Generated OpenAPI document: {outputPath}");
			return 0;
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine("Failed to generate OpenAPI document.");
			Console.Error.WriteLine(ex);
			return 1;
		}
	}

	private static string ResolveOutputPath(string[] args)
	{
		if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
		{
			return Path.GetFullPath(args[0]);
		}

		var repoRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".."));
		return Path.Combine(repoRoot, "SolarWinds.ServiceDesk.OpenApi.json");
	}

	private static JsonObject BuildOpenApiDocument()
	{
		var apiVersion = GetApiVersion();
		var paths = new JsonObject();
		var components = new JsonObject();
		var schemas = new JsonObject();
		var securitySchemes = new JsonObject
		{
			["X-Samanage-Authorization"] = new JsonObject
			{
				["type"] = "apiKey",
				["in"] = "header",
				["name"] = "X-Samanage-Authorization",
				["description"] = "Service Desk API token header, e.g. Bearer <token>."
			}
		};

		components["schemas"] = schemas;
		components["securitySchemes"] = securitySchemes;

		var schemaBuilder = new SchemaBuilder(schemas);

		var assembly = typeof(SolarWindsServiceDeskClient).Assembly;
		var interfaces = assembly.GetTypes()
			.Where(static t =>
				t.IsInterface &&
				t.Namespace is "SolarWinds.Api.ServiceDesk.Interfaces")
			.OrderBy(static t => t.Name, StringComparer.Ordinal)
			.ToArray();

		foreach (var iface in interfaces)
		{
			BuildInterfaceOperations(iface, paths, schemaBuilder);
		}

		return new JsonObject
		{
			["openapi"] = "3.1.1",
			["info"] = new JsonObject
			{
				["title"] = "SolarWinds Service Desk API",
				["version"] = apiVersion,
				["description"] = "Generated from SolarWinds.Api Refit interfaces and models."
			},
			["jsonSchemaDialect"] = "https://json-schema.org/draft/2020-12/schema",
			["servers"] = new JsonArray
			{
				new JsonObject
				{
					["url"] = "https://api.samanage.com",
					["description"] = "SolarWinds Service Desk (US)"
				},
				new JsonObject
				{
					["url"] = "https://apieu.samanage.com",
					["description"] = "SolarWinds Service Desk (EU)"
				},
				new JsonObject
				{
					["url"] = "https://apiau.samanage.com",
					["description"] = "SolarWinds Service Desk (APJ)"
				}
			},
			["paths"] = paths,
			["components"] = components,
			["security"] = new JsonArray
			{
				new JsonObject
				{
					["X-Samanage-Authorization"] = new JsonArray()
				}
			}
		};
	}

	private static string GetApiVersion()
	{
		var assembly = typeof(SolarWindsServiceDeskClient).Assembly;
		var informationalVersion = assembly
			.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
			?.InformationalVersion;

		if (string.IsNullOrWhiteSpace(informationalVersion))
		{
			return "0.0.0";
		}

		var plusIndex = informationalVersion.IndexOf('+');
		return plusIndex >= 0
			? informationalVersion[..plusIndex]
			: informationalVersion;
	}

	private static void BuildInterfaceOperations(Type iface, JsonObject paths, SchemaBuilder schemaBuilder)
	{
		var methods = iface.GetMethods(BindingFlags.Public | BindingFlags.Instance)
			.OrderBy(static m => m.Name, StringComparer.Ordinal)
			.ThenBy(static m => m.ToString(), StringComparer.Ordinal)
			.ToArray();

		foreach (var method in methods)
		{
			var methodAttribute = GetHttpMethodAttribute(method);
			if (methodAttribute is null)
			{
				continue;
			}

			var route = methodAttribute.Path ?? "/";
			var (path, fixedQueryParameters) = SplitRouteAndFixedQuery(route);
			var normalizedPath = NormalizePath(path);

			if (paths[normalizedPath] is not JsonObject pathItem)
			{
				pathItem = new JsonObject();
				paths[normalizedPath] = pathItem;
			}

			var operation = BuildOperation(iface, method, methodAttribute, normalizedPath, fixedQueryParameters, schemaBuilder);
			pathItem[methodAttribute.Method.Method.ToLowerInvariant()] = operation;
		}
	}

	private static JsonObject BuildOperation(
		Type iface,
		MethodInfo method,
		HttpMethodAttribute httpMethod,
		string normalizedPath,
		IReadOnlyDictionary<string, string> fixedQueryParameters,
		SchemaBuilder schemaBuilder)
	{
		var parameters = new JsonArray();
		var bodyParameter = default(ParameterInfo);
		var placeholders = ExtractPathPlaceholders(normalizedPath);
		var seenParameters = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		foreach (var fixedParam in fixedQueryParameters)
		{
			var key = $"query:{fixedParam.Key}";
			if (!seenParameters.Add(key))
			{
				continue;
			}

			parameters.Add(new JsonObject
			{
				["name"] = fixedParam.Key,
				["in"] = "query",
				["required"] = true,
				["schema"] = new JsonObject
				{
					["type"] = "string",
					["enum"] = new JsonArray(fixedParam.Value)
				}
			});
		}

		foreach (var parameter in method.GetParameters())
		{
			if (parameter.ParameterType == typeof(CancellationToken))
			{
				continue;
			}

			if (parameter.GetCustomAttribute<BodyAttribute>() is not null)
			{
				bodyParameter = parameter;
				continue;
			}

			var queryAttribute = parameter.GetCustomAttribute<QueryAttribute>();
			if (queryAttribute is not null && IsComplexQueryObject(parameter.ParameterType))
			{
				foreach (var queryProperty in GetPublicSchemaProperties(parameter.ParameterType))
				{
					var queryName = GetSerializedName(queryProperty);
					var key = $"query:{queryName}";
					if (!seenParameters.Add(key))
					{
						continue;
					}

					parameters.Add(new JsonObject
					{
						["name"] = queryName,
						["in"] = "query",
						["required"] = IsRequired(queryProperty),
						["schema"] = schemaBuilder.GetSchema(queryProperty.PropertyType)
					});
				}

				continue;
			}

			var parameterName = GetSerializedName(parameter);
			var inPath = placeholders.Contains(parameterName, StringComparer.OrdinalIgnoreCase);
			var location = inPath ? "path" : "query";
			var keyForSet = $"{location}:{parameterName}";
			if (!seenParameters.Add(keyForSet))
			{
				continue;
			}

			parameters.Add(new JsonObject
			{
				["name"] = parameterName,
				["in"] = location,
				["required"] = inPath || IsRequired(parameter),
				["schema"] = schemaBuilder.GetSchema(parameter.ParameterType)
			});
		}

		var responses = BuildResponses(method.ReturnType, schemaBuilder);
		var operation = new JsonObject
		{
			["operationId"] = BuildOperationId(iface, method),
			["responses"] = responses
		};

		if (parameters.Count > 0)
		{
			operation["parameters"] = parameters;
		}

		if (bodyParameter is not null)
		{
			operation["requestBody"] = new JsonObject
			{
				["required"] = true,
				["content"] = new JsonObject
				{
					["application/json"] = new JsonObject
					{
						["schema"] = schemaBuilder.GetSchema(bodyParameter.ParameterType)
					}
				}
			};
		}

		return operation;
	}

	private static JsonObject BuildResponses(Type returnType, SchemaBuilder schemaBuilder)
	{
		if (returnType == typeof(Task) || returnType == typeof(ValueTask))
		{
			return new JsonObject
			{
				["204"] = new JsonObject
				{
					["description"] = "No content"
				}
			};
		}

		Type? payloadType = null;
		if (returnType.IsGenericType)
		{
			var genericTypeDef = returnType.GetGenericTypeDefinition();
			if (genericTypeDef == typeof(Task<>) || genericTypeDef == typeof(ValueTask<>))
			{
				payloadType = returnType.GetGenericArguments()[0];
			}
		}

		if (payloadType is null)
		{
			return new JsonObject
			{
				["200"] = new JsonObject
				{
					["description"] = "OK"
				}
			};
		}

		return new JsonObject
		{
			["200"] = new JsonObject
			{
				["description"] = "OK",
				["content"] = new JsonObject
				{
					["application/json"] = new JsonObject
					{
						["schema"] = schemaBuilder.GetSchema(payloadType)
					}
				}
			}
		};
	}

	private static string BuildOperationId(Type iface, MethodInfo method)
	{
		var overloadSuffix = string.Join("_", method.GetParameters()
			.Where(static p => p.ParameterType != typeof(CancellationToken))
			.Select(static p => p.Name)
			.Where(static n => !string.IsNullOrWhiteSpace(n)));
		return string.IsNullOrWhiteSpace(overloadSuffix)
			? $"{iface.Name}_{method.Name}"
			: $"{iface.Name}_{method.Name}_{overloadSuffix}";
	}

	private static HttpMethodAttribute? GetHttpMethodAttribute(MethodInfo method)
		=> method.GetCustomAttributes().OfType<HttpMethodAttribute>().FirstOrDefault();

	private static bool IsComplexQueryObject(Type type)
	{
		var normalized = Nullable.GetUnderlyingType(type) ?? type;
		if (normalized == typeof(string) || normalized.IsPrimitive || normalized.IsEnum)
		{
			return false;
		}

		if (normalized == typeof(DateTime) || normalized == typeof(DateTimeOffset) || normalized == typeof(Guid) || normalized == typeof(decimal))
		{
			return false;
		}

		if (typeof(IEnumerable).IsAssignableFrom(normalized) && normalized != typeof(string))
		{
			return false;
		}

		return normalized.IsClass || (normalized.IsValueType && !normalized.IsPrimitive && !normalized.IsEnum);
	}

	private static (string Path, IReadOnlyDictionary<string, string> QueryParameters) SplitRouteAndFixedQuery(string route)
	{
		var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		var split = route.Split('?', 2, StringSplitOptions.None);
		if (split.Length == 2)
		{
			foreach (var pair in split[1].Split('&', StringSplitOptions.RemoveEmptyEntries))
			{
				var kvp = pair.Split('=', 2, StringSplitOptions.None);
				if (kvp.Length == 2)
				{
					result[kvp[0]] = Uri.UnescapeDataString(kvp[1]);
				}
			}
		}

		return (split[0], result);
	}

	private static string NormalizePath(string path)
		=> string.IsNullOrWhiteSpace(path)
			? "/"
			: path.StartsWith('/') ? path : "/" + path;

	private static string[] ExtractPathPlaceholders(string path)
		=> PathPlaceholderRegex().Matches(path).Select(static match => match.Groups[1].Value).ToArray();

	private static string GetSerializedName(ParameterInfo parameter)
	{
		var alias = parameter.GetCustomAttribute<AliasAsAttribute>()?.Name;
		if (!string.IsNullOrWhiteSpace(alias))
		{
			return alias;
		}

		return parameter.Name ?? "value";
	}

	private static string GetSerializedName(PropertyInfo property)
	{
		var alias = property.GetCustomAttribute<AliasAsAttribute>()?.Name;
		if (!string.IsNullOrWhiteSpace(alias))
		{
			return alias;
		}

		var stj = property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name;
		if (!string.IsNullOrWhiteSpace(stj))
		{
			return stj;
		}

		var dataMember = property.GetCustomAttribute<DataMemberAttribute>()?.Name;
		if (!string.IsNullOrWhiteSpace(dataMember))
		{
			return dataMember;
		}

		return ToSnakeCase(property.Name);
	}

	private static bool IsRequired(PropertyInfo property)
	{
		if (property.GetCustomAttribute<JsonIgnoreAttribute>() is { Condition: JsonIgnoreCondition.Always })
		{
			return false;
		}

		if (property.GetCustomAttribute<RequiredMemberAttribute>() is not null)
		{
			return true;
		}

		var nullability = NullabilityContext.Create(property);
		if (nullability.WriteState == NullabilityState.NotNull)
		{
			return true;
		}

		return property.PropertyType.IsValueType && Nullable.GetUnderlyingType(property.PropertyType) is null;
	}

	private static bool IsRequired(ParameterInfo parameter)
	{
		var nullability = NullabilityContext.Create(parameter);
		if (nullability.WriteState == NullabilityState.NotNull)
		{
			return true;
		}

		return parameter.ParameterType.IsValueType && Nullable.GetUnderlyingType(parameter.ParameterType) is null;
	}

	private static IEnumerable<PropertyInfo> GetPublicSchemaProperties(Type type)
		=> type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.Where(static p => p.GetMethod is not null && !p.GetMethod.IsStatic)
			.OrderBy(static p => p.Name, StringComparer.Ordinal);

	private static string ToSnakeCase(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			return name;
		}

		var chars = new List<char>(name.Length + 8);
		for (var i = 0; i < name.Length; i++)
		{
			var c = name[i];
			if (char.IsUpper(c))
			{
				if (i > 0 && (char.IsLower(name[i - 1]) || (i + 1 < name.Length && char.IsLower(name[i + 1]))))
				{
					chars.Add('_');
				}
				chars.Add(char.ToLowerInvariant(c));
			}
			else
			{
				chars.Add(c);
			}
		}

		return new string(chars.ToArray());
	}

	[GeneratedRegex("\\{([^}]+)\\}")]
	private static partial Regex PathPlaceholderRegex();

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

		private static JsonObject? TryBuildInlineSchema(Type type)
		{
			if (type == typeof(string))
			{
				return new JsonObject { ["type"] = "string" };
			}

			if (type == typeof(bool))
			{
				return new JsonObject { ["type"] = "boolean" };
			}

			if (type == typeof(byte) || type == typeof(short) || type == typeof(int))
			{
				return new JsonObject { ["type"] = "integer", ["format"] = "int32" };
			}

			if (type == typeof(long))
			{
				return new JsonObject { ["type"] = "integer", ["format"] = "int64" };
			}

			if (type == typeof(float))
			{
				return new JsonObject { ["type"] = "number", ["format"] = "float" };
			}

			if (type == typeof(double) || type == typeof(decimal))
			{
				return new JsonObject { ["type"] = "number", ["format"] = "double" };
			}

			if (type == typeof(DateTime) || type == typeof(DateTimeOffset))
			{
				return new JsonObject { ["type"] = "string", ["format"] = "date-time" };
			}

			if (type == typeof(DateOnly))
			{
				return new JsonObject { ["type"] = "string", ["format"] = "date" };
			}

			if (type == typeof(TimeOnly) || type == typeof(TimeSpan))
			{
				return new JsonObject { ["type"] = "string" };
			}

			if (type == typeof(Guid))
			{
				return new JsonObject { ["type"] = "string", ["format"] = "uuid" };
			}

			if (type == typeof(Uri))
			{
				return new JsonObject { ["type"] = "string", ["format"] = "uri" };
			}

			if (type.IsEnum)
			{
				return null;
			}

			return null;
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

			if (type.IsGenericType)
			{
				var args = type.GetGenericArguments();
				if (args.Length == 1)
				{
					elementType = args[0];
					return true;
				}
			}

			var enumerableInterface = type.GetInterfaces()
				.FirstOrDefault(static i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
			if (enumerableInterface is null)
			{
				return false;
			}

			elementType = enumerableInterface.GetGenericArguments()[0];
			return true;
		}

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
