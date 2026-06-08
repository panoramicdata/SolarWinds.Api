using System.Text.Json;
using System.Text.Json.Serialization;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents the unseen notification count returned by the notifications API.
/// </summary>
[JsonConverter(typeof(NotificationUnseenCountJsonConverter))]
public class NotificationUnseenCount
{
	/// <summary>
	/// Gets or sets the number of unseen notifications.
	/// </summary>
	[JsonPropertyName("unseen_count")]
	public int UnseenCount { get; set; }
}

internal sealed class NotificationUnseenCountJsonConverter : JsonConverter<NotificationUnseenCount>
{
	public override NotificationUnseenCount Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.Number)
		{
			return new NotificationUnseenCount { UnseenCount = reader.GetInt32() };
		}

		if (reader.TokenType == JsonTokenType.StartObject)
		{
			using var document = JsonDocument.ParseValue(ref reader);
			var root = document.RootElement;
			if (root.TryGetProperty("unseen_count", out var unseenCountElement) && unseenCountElement.ValueKind == JsonValueKind.Number)
			{
				return new NotificationUnseenCount { UnseenCount = unseenCountElement.GetInt32() };
			}

			if (root.TryGetProperty("count", out var countElement) && countElement.ValueKind == JsonValueKind.Number)
			{
				return new NotificationUnseenCount { UnseenCount = countElement.GetInt32() };
			}
		}

		throw new JsonException("Unsupported notifications unseen-count payload format.");
	}

	public override void Write(
		Utf8JsonWriter writer,
		NotificationUnseenCount value,
		JsonSerializerOptions options)
		=> writer.WriteNumberValue(value.UnseenCount);
}
