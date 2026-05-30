using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a custom field definition returned by the custom_fields API.
/// </summary>
public class CustomFieldDefinition : BaseEntity
{
	/// <summary>
	/// Gets or sets the field name.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the field title/label shown in the UI.
	/// </summary>
	[JsonPropertyName("title")]
	public string? Title { get; set; }

	/// <summary>
	/// Gets or sets the field type (for example "text", "dropdown", "date").
	/// </summary>
	[JsonPropertyName("field_type")]
	public string? FieldType { get; set; }

	/// <summary>
	/// Gets or sets the model this field belongs to (for example "Incident", "Problem").
	/// </summary>
	[JsonPropertyName("model")]
	public string? Model { get; set; }

	/// <summary>
	/// Gets or sets whether the field is required.
	/// </summary>
	[JsonPropertyName("required")]
	public bool Required { get; set; }

	/// <summary>
	/// Gets or sets the position/sort order.
	/// </summary>
	[JsonPropertyName("position")]
	public int? Position { get; set; }

	/// <summary>
	/// Gets or sets the available options for dropdown/select fields.
	/// </summary>
	[JsonPropertyName("options")]
	public List<CustomFieldOption>? Options { get; set; }
}

/// <summary>
/// Represents a selectable option for a dropdown custom field.
/// </summary>
public class CustomFieldOption : BaseEntity
{
	/// <summary>
	/// Gets or sets the option value.
	/// </summary>
	[JsonPropertyName("value")]
	public string Value { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the option position.
	/// </summary>
	[JsonPropertyName("position")]
	public int? Position { get; set; }
}

/// <summary>
/// Represents a potential dynamic approval returned by the custom fields API.
/// </summary>
public class PotentialDynamicApproval
{
	/// <summary>
	/// Gets or sets the custom field ID.
	/// </summary>
	[JsonPropertyName("id")]
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the field name.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the model the field belongs to.
	/// </summary>
	[JsonPropertyName("model")]
	public string? Model { get; set; }

	/// <summary>
	/// Gets or sets the field type.
	/// </summary>
	[JsonPropertyName("field_type")]
	public string? FieldType { get; set; }
}
