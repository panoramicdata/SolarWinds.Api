using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using Refit;

namespace SolarWinds.Api.ServiceDesk.Helpers;

/// <summary>
/// Formats URL parameters to match Service Desk API conventions.
/// </summary>
public sealed class ServiceDeskUrlParameterFormatter : IUrlParameterFormatter
{
	private const string ShortMonthDayYearFormat = "MMM d yyyy";

	/// <inheritdoc />
	public string? Format(object? value, ICustomAttributeProvider attributeProvider, Type type)
	{
		if (value is null)
		{
			return null;
		}

		if (value is DateTime dateTime)
		{
			return dateTime.ToString(ShortMonthDayYearFormat, CultureInfo.InvariantCulture);
		}

		if (value is DateTimeOffset dateTimeOffset)
		{
			return dateTimeOffset.ToString(ShortMonthDayYearFormat, CultureInfo.InvariantCulture);
		}

		if (value is Enum enumValue)
		{
			return GetEnumString(enumValue);
		}

		return value.ToString();
	}

	private static string GetEnumString(Enum enumValue)
	{
		var memberName = enumValue.ToString();
		var member = enumValue.GetType().GetMember(memberName).FirstOrDefault();
		var enumMember = member?.GetCustomAttribute<EnumMemberAttribute>();

		if (!string.IsNullOrWhiteSpace(enumMember?.Value))
		{
			return enumMember.Value;
		}

		return memberName.ToLowerInvariant();
	}
}