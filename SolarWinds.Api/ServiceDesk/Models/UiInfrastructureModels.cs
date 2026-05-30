using Refit;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Common portal-mode query flag.
/// </summary>
public sealed class PortalModeRequest
{
	/// <summary>
	/// Gets or sets whether the request should be evaluated in portal mode.
	/// </summary>
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

/// <summary>
/// Query parameters for the shared custom-context endpoint.
/// </summary>
public sealed class UiCustomContextRequest
{
	/// <summary>
	/// Gets or sets the UI context key.
	/// </summary>
	[AliasAs("context")]
	public string? Context { get; set; }

	/// <summary>
	/// Gets or sets whether the request should be evaluated in portal mode.
	/// </summary>
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

/// <summary>
/// Query parameters for URL-filter resolution endpoints.
/// </summary>
public sealed class UiUrlFiltersRequest
{
	/// <summary>
	/// Gets or sets the current URL query string.
	/// </summary>
	[AliasAs("query_string")]
	public string? QueryString { get; set; }

	/// <summary>
	/// Gets or sets the UI context key.
	/// </summary>
	[AliasAs("context")]
	public string? Context { get; set; }

	/// <summary>
	/// Gets or sets whether the request should be evaluated in portal mode.
	/// </summary>
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

/// <summary>
/// Query parameters for widget definition endpoints.
/// </summary>
public sealed class UiWidgetRequest
{
	/// <summary>
	/// Gets or sets the real-time connection identifier.
	/// </summary>
	[AliasAs("connection_id")]
	public string? ConnectionId { get; set; }

	/// <summary>
	/// Gets or sets whether ELS is enabled.
	/// </summary>
	[AliasAs("els_enabled")]
	public bool? ElsEnabled { get; set; }

	/// <summary>
	/// Gets or sets the update sequence/count marker.
	/// </summary>
	[AliasAs("numOfUpdates")]
	public int? NumOfUpdates { get; set; }

	/// <summary>
	/// Gets or sets whether the request should be evaluated in portal mode.
	/// </summary>
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

/// <summary>
/// Query parameters for widget data endpoints.
/// </summary>
public sealed class UiWidgetDataRequest
{
	/// <summary>
	/// Gets or sets widget type.
	/// </summary>
	[AliasAs("type")]
	public string? Type { get; set; }

	/// <summary>
	/// Gets or sets widget settings payload.
	/// </summary>
	[AliasAs("settings")]
	public string? Settings { get; set; }

	/// <summary>
	/// Gets or sets widget identifier.
	/// </summary>
	[AliasAs("widget_id")]
	public int? WidgetId { get; set; }

	/// <summary>
	/// Gets or sets filter payload.
	/// </summary>
	[AliasAs("filters")]
	public string? Filters { get; set; }

	/// <summary>
	/// Gets or sets real-time connection identifier.
	/// </summary>
	[AliasAs("connection_id")]
	public string? ConnectionId { get; set; }

	/// <summary>
	/// Gets or sets whether the request should be evaluated in portal mode.
	/// </summary>
	[AliasAs("is_portal_mode")]
	public bool? IsPortalMode { get; set; }
}

/// <summary>
/// Dynamic response wrapper for custom context data.
/// </summary>
public sealed class UiCustomContextResponse
{
	/// <summary>
	/// Gets or sets additional unmodeled response fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

/// <summary>
/// Dynamic response wrapper for URL filter data.
/// </summary>
public sealed class UiUrlFiltersResponse
{
	/// <summary>
	/// Gets or sets additional unmodeled response fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

/// <summary>
/// Dashboard summary payload.
/// </summary>
public sealed class UiDashboardSummary
{
	/// <summary>
	/// Gets or sets dashboard identifier.
	/// </summary>
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	/// <summary>
	/// Gets or sets dashboard name.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets additional unmodeled dashboard fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

/// <summary>
/// Dashboard detail payload.
/// </summary>
public sealed class UiDashboardDetail
{
	/// <summary>
	/// Gets or sets dashboard identifier.
	/// </summary>
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	/// <summary>
	/// Gets or sets dashboard name.
	/// </summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>
	/// Gets or sets additional unmodeled dashboard fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

/// <summary>
/// Widget definition payload.
/// </summary>
public sealed class UiWidgetDefinition
{
	/// <summary>
	/// Gets or sets widget identifier.
	/// </summary>
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	/// <summary>
	/// Gets or sets widget type.
	/// </summary>
	[JsonPropertyName("type")]
	public string? Type { get; set; }

	/// <summary>
	/// Gets or sets additional unmodeled widget fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

/// <summary>
/// Dynamic response wrapper for widget data.
/// </summary>
public sealed class UiWidgetDataResponse
{
	/// <summary>
	/// Gets or sets additional unmodeled response fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}

/// <summary>
/// Response payload containing a WebSocket JWT token.
/// </summary>
public sealed class UiWebsocketJwtResponse
{
	/// <summary>
	/// Gets or sets the JWT token value.
	/// </summary>
	[JsonPropertyName("jwt")]
	public string? Jwt { get; set; }

	/// <summary>
	/// Gets or sets additional unmodeled response fields.
	/// </summary>
	[JsonExtensionData]
	public Dictionary<string, object?>? ExtensionData { get; set; }
}
