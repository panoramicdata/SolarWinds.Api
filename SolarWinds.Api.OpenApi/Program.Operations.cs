using System.Reflection;
using System.Text.Json.Nodes;
using Refit;

namespace SolarWinds.Api.OpenApi;

internal static partial class Program
{
	private static void BuildInterfaceOperations(Type iface, JsonObject paths, SchemaBuilder schemaBuilder, ISet<string> documentTags)
	{
		var tagName = BuildTagName(iface);
		documentTags.Add(tagName);

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

			var operation = BuildOperation(iface, method, normalizedPath, fixedQueryParameters, schemaBuilder, tagName);
			pathItem[methodAttribute.Method.Method.ToLowerInvariant()] = operation;
		}
	}

	private static JsonObject BuildOperation(
		Type iface,
		MethodInfo method,
		string normalizedPath,
		IReadOnlyDictionary<string, string> fixedQueryParameters,
		SchemaBuilder schemaBuilder,
		string tagName)
	{
		var parameters = new JsonArray();
		var seenParameters = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		AddFixedQueryParameters(parameters, seenParameters, fixedQueryParameters);
		var bodyParameter = AddMethodParameters(parameters, seenParameters, method, normalizedPath, schemaBuilder);

		var operation = new JsonObject
		{
			["operationId"] = BuildOperationId(iface, method),
			["tags"] = new JsonArray(tagName),
			["responses"] = BuildResponses(method.ReturnType, schemaBuilder)
		};

		if (parameters.Count > 0)
		{
			operation["parameters"] = parameters;
		}

		if (bodyParameter is not null)
		{
			operation["requestBody"] = BuildRequestBody(bodyParameter, schemaBuilder);
		}

		return operation;
	}

	/// <summary>
	/// Adds the query parameters that the route itself pins to a literal value, such as a
	/// "layout=long" written into a Refit path. Each is described as a single-value enum.
	/// </summary>
	private static void AddFixedQueryParameters(
		JsonArray parameters,
		HashSet<string> seenParameters,
		IReadOnlyDictionary<string, string> fixedQueryParameters)
	{
		foreach (var fixedParameter in fixedQueryParameters)
		{
			if (!seenParameters.Add($"query:{fixedParameter.Key}"))
			{
				continue;
			}

			parameters.Add(new JsonObject
			{
				["name"] = fixedParameter.Key,
				["in"] = "query",
				["required"] = true,
				["schema"] = new JsonObject
				{
					["type"] = "string",
					["enum"] = new JsonArray(fixedParameter.Value)
				}
			});
		}
	}

	/// <summary>
	/// Describes each of the method own parameters, and returns the one carrying the request
	/// body, if the method has one.
	/// </summary>
	private static ParameterInfo? AddMethodParameters(
		JsonArray parameters,
		HashSet<string> seenParameters,
		MethodInfo method,
		string normalizedPath,
		SchemaBuilder schemaBuilder)
	{
		var placeholders = ExtractPathPlaceholders(normalizedPath);
		ParameterInfo? bodyParameter = null;

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

			if (parameter.GetCustomAttribute<QueryAttribute>() is not null && IsComplexQueryObject(parameter.ParameterType))
			{
				AddExpandedQueryParameters(parameters, seenParameters, parameter.ParameterType, schemaBuilder);
				continue;
			}

			AddPathOrQueryParameter(parameters, seenParameters, parameter, placeholders, schemaBuilder);
		}

		return bodyParameter;
	}

	/// <summary>
	/// Expands a [Query] object parameter, whose properties Refit flattens into one query
	/// parameter each rather than sending the object as a single value.
	/// </summary>
	private static void AddExpandedQueryParameters(
		JsonArray parameters,
		HashSet<string> seenParameters,
		Type queryObjectType,
		SchemaBuilder schemaBuilder)
	{
		foreach (var queryProperty in GetPublicSchemaProperties(queryObjectType))
		{
			var queryName = GetSerializedName(queryProperty);
			if (!seenParameters.Add($"query:{queryName}"))
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
	}

	/// <summary>
	/// Describes a scalar parameter, placing it in the path when the route has a matching
	/// placeholder and in the query string otherwise.
	/// </summary>
	private static void AddPathOrQueryParameter(
		JsonArray parameters,
		HashSet<string> seenParameters,
		ParameterInfo parameter,
		string[] placeholders,
		SchemaBuilder schemaBuilder)
	{
		var parameterName = GetSerializedName(parameter);
		var inPath = placeholders.Contains(parameterName, StringComparer.OrdinalIgnoreCase);
		var location = inPath ? "path" : "query";
		if (!seenParameters.Add($"{location}:{parameterName}"))
		{
			return;
		}

		parameters.Add(new JsonObject
		{
			["name"] = parameterName,
			["in"] = location,
			["required"] = inPath || IsRequired(parameter),
			["schema"] = schemaBuilder.GetSchema(parameter.ParameterType)
		});
	}

	private static JsonObject BuildRequestBody(ParameterInfo bodyParameter, SchemaBuilder schemaBuilder)
		=> new()
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
}
