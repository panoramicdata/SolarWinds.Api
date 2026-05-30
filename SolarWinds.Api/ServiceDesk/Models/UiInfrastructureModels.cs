using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

public sealed class PortalModeRequest
{
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

public sealed class UiCustomContextRequest
{
	[AliasAs("context")]
	public string? Context { get; set; }

	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

public sealed class UiUrlFiltersRequest
{
	[AliasAs("query_string")]
	public string? QueryString { get; set; }

	[AliasAs("context")]
	public string? Context { get; set; }

	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

public sealed class UiWidgetRequest
{
	[AliasAs("connection_id")]
	public string? ConnectionId { get; set; }

	[AliasAs("els_enabled")]
	public bool? ElsEnabled { get; set; }

	[AliasAs("numOfUpdates")]
	public int? NumOfUpdates { get; set; }

	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

public sealed class UiWidgetDataRequest
{
	[AliasAs("type")]
	public string? Type { get; set; }

	[AliasAs("settings")]
	public string? Settings { get; set; }

	[AliasAs("widget_id")]
	public int? WidgetId { get; set; }

	[AliasAs("filters")]
	public string? Filters { get; set; }

	[AliasAs("connection_id")]
	public string? ConnectionId { get; set; }

	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

public sealed class UiCustomContextResponse
{
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

public sealed class UiUrlFiltersResponse
{
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

public sealed class UiDashboardSummary
{
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

public sealed class UiDashboardDetail
{
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

public sealed class UiWidgetDefinition
{
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	[JsonPropertyName("type")]
	public string? Type { get; set; }

	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

public sealed class UiWidgetDataResponse
{
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

public sealed class UiWebsocketJwtResponse
{
	[JsonPropertyName("jwt")]
	public string? Jwt { get; set; }

	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}
