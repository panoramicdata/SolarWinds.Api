using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Refit;

namespace SolarWinds.Api.OpenApi;

internal static partial class Program
{
	private static readonly NullabilityInfoContext NullabilityContext = new();

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

	private static string BuildTagName(Type iface)
	{
		var name = iface.Name;
		if (name.StartsWith('I') && name.Length > 1 && char.IsUpper(name[1]))
		{
			name = name[1..];
		}

		if (name.EndsWith("Api", StringComparison.Ordinal) && name.Length > 3)
		{
			name = name[..^3];
		}

		return HumanizePascalCase(name);
	}

	private static string HumanizePascalCase(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return value;
		}

		var chars = new List<char>(value.Length + 8);
		for (var i = 0; i < value.Length; i++)
		{
			var c = value[i];
			if (i > 0 && char.IsUpper(c) && (char.IsLower(value[i - 1]) || (i + 1 < value.Length && char.IsLower(value[i + 1]))))
			{
				chars.Add(' ');
			}

			chars.Add(c);
		}

		return new string(chars.ToArray());
	}

	private static HttpMethodAttribute? GetHttpMethodAttribute(MethodInfo method)
		=> method.GetCustomAttributes().OfType<HttpMethodAttribute>().FirstOrDefault();

	/// <summary>
	/// The types a query parameter of which is sent as a single scalar value. Anything else that
	/// is an object has its properties flattened into one query parameter each.
	/// </summary>
	private static readonly HashSet<Type> ScalarQueryTypes =
	[
		typeof(string),
		typeof(DateTime),
		typeof(DateTimeOffset),
		typeof(Guid),
		typeof(decimal)
	];

	private static bool IsComplexQueryObject(Type type)
	{
		var normalized = Nullable.GetUnderlyingType(type) ?? type;

		if (normalized.IsPrimitive || normalized.IsEnum || ScalarQueryTypes.Contains(normalized))
		{
			return false;
		}

		if (typeof(IEnumerable).IsAssignableFrom(normalized))
		{
			return false;
		}

		return normalized.IsClass || normalized.IsValueType;
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
}
