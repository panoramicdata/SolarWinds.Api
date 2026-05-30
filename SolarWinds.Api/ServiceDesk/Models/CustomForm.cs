using SolarWinds.Api.ServiceDesk.Models.Base;

namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a custom form definition returned by the setup/custom_forms API.
/// </summary>
public class CustomForm : BaseEntity
{
	/// <summary>
	/// Gets or sets the form name.
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the model type this form applies to (for example "Incident").
	/// </summary>
	public string? ModelType { get; set; }

	/// <summary>
	/// Gets or sets the form fields.
	/// </summary>
	[JsonPropertyName("custom_forms_fields")]
	public List<CustomFormField> Fields { get; set; } = [];

	/// <summary>
	/// Gets or sets whether the form is active.
	/// </summary>
	public bool Active { get; set; }
}

/// <summary>
/// Represents a single field within a custom form.
/// </summary>
public class CustomFormField : BaseEntity
{
	/// <summary>
	/// Gets or sets the field label.
	/// </summary>
	public string? Label { get; set; }

	/// <summary>
	/// Gets or sets the field type.
	/// </summary>
	public string? FieldType { get; set; }

	/// <summary>
	/// Gets or sets whether the field is required.
	/// </summary>
	public bool Required { get; set; }

	/// <summary>
	/// Gets or sets the field position/order.
	/// </summary>
	public int? Position { get; set; }

	/// <summary>
	/// Gets or sets the custom field ID this form field maps to.
	/// </summary>
	public int? CustomFieldId { get; set; }
}
