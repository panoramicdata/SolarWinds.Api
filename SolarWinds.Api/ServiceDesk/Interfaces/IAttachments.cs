using System.Threading;
using System.Threading.Tasks;
using Refit;
using SolarWinds.Api.ServiceDesk.Models;

namespace SolarWinds.Api.ServiceDesk.Interfaces;
/// <summary>
/// Interface for the Service Desk Attachments API.
/// Note: The API documentation only supports creation of attachments.
/// </summary>
public interface IAttachments
{
	/// <summary>
	/// Creates a new attachment.
	/// </summary>
	/// <param name="attachment">The attachment to create.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The created attachment.</returns>
	[Post("/api/v2/attachments.json")]
	public Task<Attachment> CreateAsync(
		[Body] Attachment attachment,
		CancellationToken cancellationToken);
}