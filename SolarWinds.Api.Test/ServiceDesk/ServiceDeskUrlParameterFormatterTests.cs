using System.Runtime.Serialization;
using System.Reflection;

namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class ServiceDeskUrlParameterFormatterTests
{
	private static readonly ServiceDeskUrlParameterFormatter Formatter = new();
	private static readonly ICustomAttributeProvider AttributeProvider = typeof(GetIncidentsRequest)
		.GetProperty(nameof(GetIncidentsRequest.Layout))!;

	/// <summary>
	/// Executes Format_WithNullValue_ReturnsNull.
	/// </summary>
	[Fact]
	public void Format_WithNullValue_ReturnsNull()
	{
		var result = Formatter.Format(null, AttributeProvider, typeof(string));

		result.Should().BeNull();
	}

	/// <summary>
	/// Executes Format_WithDateTime_UsesShortMonthDayYearInvariantFormat.
	/// </summary>
	[Fact]
	public void Format_WithDateTime_UsesShortMonthDayYearInvariantFormat()
	{
		var result = Formatter.Format(new DateTime(2026, 1, 5, 23, 59, 59), AttributeProvider, typeof(DateTime));

		result.Should().Be("Jan 5 2026");
	}

	/// <summary>
	/// Executes Format_WithDateTimeOffset_UsesShortMonthDayYearInvariantFormat.
	/// </summary>
	[Fact]
	public void Format_WithDateTimeOffset_UsesShortMonthDayYearInvariantFormat()
	{
		var value = new DateTimeOffset(2026, 12, 31, 22, 0, 0, TimeSpan.FromHours(-5));
		var result = Formatter.Format(value, AttributeProvider, typeof(DateTimeOffset));

		result.Should().Be("Dec 31 2026");
	}

	/// <summary>
	/// Executes Format_WithEnumMember_UsesEnumMemberValue.
	/// </summary>
	[Fact]
	public void Format_WithEnumMember_UsesEnumMemberValue()
	{
		var result = Formatter.Format(ResponseLayout.Long, AttributeProvider, typeof(ResponseLayout));

		result.Should().Be("long");
	}

	/// <summary>
	/// Executes Format_WithEnumWithoutEnumMember_UsesLowercaseName.
	/// </summary>
	[Fact]
	public void Format_WithEnumWithoutEnumMember_UsesLowercaseName()
	{
		var result = Formatter.Format(LocalStatus.WaitingApproval, AttributeProvider, typeof(LocalStatus));

		result.Should().Be("waitingapproval");
	}

	/// <summary>
	/// Executes Format_WithNonSpecialType_UsesToString.
	/// </summary>
	[Fact]
	public void Format_WithNonSpecialType_UsesToString()
	{
		var result = Formatter.Format(42, AttributeProvider, typeof(int));

		result.Should().Be("42");
	}

	private enum LocalStatus
	{
		[EnumMember(Value = "ignored")]
		Done,
		WaitingApproval,
	}
}
