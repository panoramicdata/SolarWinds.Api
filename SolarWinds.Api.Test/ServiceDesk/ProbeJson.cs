namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// JSON reading helpers for the incident write-field probes.
/// </summary>
internal static class ProbeJson
{
	internal static bool TryGetJsonObjectId(object? value, out int id)
	{
		id = 0;
		if (value is null)
		{
			return false;
		}

		using var document = JsonDocument.Parse(JsonSerializer.Serialize(value));
		return TryGetObjectId(document.RootElement, out id);
	}

	internal static bool TryGetJsonArrayFirstObjectId(object? value, out int id)
	{
		id = 0;
		if (value is null)
		{
			return false;
		}

		using var document = JsonDocument.Parse(JsonSerializer.Serialize(value));
		var root = document.RootElement;
		return root.ValueKind == JsonValueKind.Array
			&& root.GetArrayLength() > 0
			&& TryGetObjectId(root[0], out id);
	}

	private static bool TryGetObjectId(JsonElement element, out int id)
	{
		id = 0;
		return element.ValueKind == JsonValueKind.Object && TryGetJsonPropertyInt32(element, "id", out id);
	}

	private static bool TryGetJsonPropertyInt32(JsonElement element, string propertyName, out int value)
	{
		foreach (var property in element.EnumerateObject())
		{
			if (string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase)
				&& property.Value.TryGetInt32(out value))
			{
				return true;
			}
		}

		value = 0;
		return false;
	}

	internal static bool TryGetJsonProperty(JsonElement element, string propertyName, out JsonElement value)
	{
		foreach (var property in element.EnumerateObject())
		{
			if (string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
			{
				value = property.Value;
				return true;
			}
		}

		value = default;
		return false;
	}

	internal static bool TryGetJsonPropertyValue(JsonElement element, string propertyName, out string value)
	{
		if (TryGetJsonProperty(element, propertyName, out var propertyValue)
			&& propertyValue.ValueKind == JsonValueKind.String
			&& propertyValue.GetString() is { } propertyString)
		{
			value = propertyString;
			return true;
		}

		value = string.Empty;
		return false;
	}

	internal static bool TryGetJsonDateTimeByProperty(JsonElement element, string propertyName, out DateTime value)
	{
		if (TryGetJsonProperty(element, propertyName, out var propertyValue)
			&& propertyValue.ValueKind == JsonValueKind.String
			&& propertyValue.TryGetDateTime(out value))
		{
			return true;
		}

		value = default;
		return false;
	}
}
