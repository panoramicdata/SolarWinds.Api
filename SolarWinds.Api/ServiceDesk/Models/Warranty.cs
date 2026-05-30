namespace SolarWinds.Api.ServiceDesk.Models;

/// <summary>
/// Represents a hardware warranty.
/// </summary>
public class Warranty : Entity
{
	/// <summary>
	/// Gets or sets service name covered by the warranty.
	/// </summary>
	public string? Service { get; set; }

	/// <summary>
	/// Gets or sets warranty provider name.
	/// </summary>
	public string? Provider { get; set; }

	/// <summary>
	/// Gets or sets warranty start date as returned by the API.
	/// </summary>
	public string? StartDate { get; set; }

	/// <summary>
	/// Gets or sets warranty end date as returned by the API.
	/// </summary>
	public string? EndDate { get; set; }

	/// <summary>
	/// Gets or sets warranty status.
	/// </summary>
	public string? Status { get; set; }

	/// <summary>
	/// Gets or sets whether the warranty was manually entered.
	/// </summary>
	public bool? Manual { get; set; }
}
