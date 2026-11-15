using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Refit;

namespace SolarWinds.Api.OpenApi;

internal static partial class Program
{
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
}
