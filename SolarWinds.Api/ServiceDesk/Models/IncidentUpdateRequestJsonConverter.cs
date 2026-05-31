using System.Text.Json;

namespace SolarWinds.Api.ServiceDesk.Models;

internal sealed class IncidentUpdateRequestJsonConverter : JsonConverter<IncidentUpdateRequest>
{
	public override IncidentUpdateRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var fields = JsonSerializer.Deserialize<IncidentUpdateFields>(ref reader, options) ?? new IncidentUpdateFields();
		return new IncidentUpdateRequest
		{
			Incident = fields
		};
	}

	public override void Write(Utf8JsonWriter writer, IncidentUpdateRequest value, JsonSerializerOptions options)
	{
		var element = JsonSerializer.SerializeToElement(value.Incident ?? new IncidentUpdateFields(), options);

		writer.WriteStartObject();
		foreach (var property in element.EnumerateObject())
		{
			if (property.Value.ValueKind != JsonValueKind.Null)
			{
				property.WriteTo(writer);
			}
		}
		writer.WriteEndObject();
	}
}