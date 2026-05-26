using System.Text.Json;
using Refit;
using SolarWinds.Api.Http;
using SolarWinds.Api.ServiceDesk.Interfaces;

namespace SolarWinds.Api;

/// <summary>
/// A client for the SolarWinds Service Desk API.
/// </summary>
public class SolarWindsServiceDeskClient
{
	private readonly HttpClient _httpClient;

	private static readonly RefitSettings _refitSettings = new(
		new SystemTextJsonContentSerializer(new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
		}));

	/// <summary>
	/// Gets the Service Desk Tickets API.
	/// </summary>
	public ITickets Tickets { get; private set; }

	/// <summary>
	/// Gets the Service Desk Users API.
	/// </summary>
	public IUsers Users { get; private set; }

	/// <summary>
	/// Gets the Service Desk Incidents API.
	/// </summary>
	public IIncidents Incidents { get; private set; }

	/// <summary>
	/// Gets the Service Desk Other Assets API.
	/// </summary>
	public IOtherAssets OtherAssets { get; private set; }

	/// <summary>
	/// Gets the Service Desk Configuration Items API.
	/// </summary>
	public IConfigurationItems ConfigurationItems { get; private set; }

	/// <summary>
	/// Gets the Service Desk Problems API.
	/// </summary>
	public IProblems Problems { get; private set; }

	/// <summary>
	/// Gets the Service Desk Changes API.
	/// </summary>
	public IChanges Changes { get; private set; }

	/// <summary>
	/// Gets the Service Desk Releases API.
	/// </summary>
	public IReleases Releases { get; private set; }

	/// <summary>
	/// Gets the Service Desk Solutions API.
	/// </summary>
	public ISolutions Solutions { get; private set; }

	/// <summary>
	/// Gets the Service Desk Catalog Items API.
	/// </summary>
	public ICatalogItems CatalogItems { get; private set; }

	/// <summary>
	/// Gets the Service Desk Service Requests API.
	/// </summary>
	public IServiceRequests ServiceRequests { get; private set; }

	/// <summary>
	/// Gets the Service Desk Sites API.
	/// </summary>
	public ISites Sites { get; private set; }

	/// <summary>
	/// Gets the Service Desk Departments API.
	/// </summary>
	public IDepartments Departments { get; private set; }

	/// <summary>
	/// Gets the Service Desk Roles API.
	/// </summary>
	public IRoles Roles { get; private set; }

	/// <summary>
	/// Gets the Service Desk Groups API.
	/// </summary>
	public IGroups Groups { get; private set; }

	/// <summary>
	/// Gets the Service Desk Categories API.
	/// </summary>
	public ICategories Categories { get; private set; }

	/// <summary>
	/// Gets the Service Desk Hardware API.
	/// </summary>
	public IHardwares Hardwares { get; private set; }

	/// <summary>
	/// Gets the Service Desk Mobile Devices API.
	/// </summary>
	public IMobileDevices MobileDevices { get; private set; }

	/// <summary>
	/// Gets the Service Desk Software API.
	/// </summary>
	public ISoftwares Softwares { get; private set; }

	/// <summary>
	/// Gets the Service Desk Printers API.
	/// </summary>
	public IPrinters Printers { get; private set; }

	/// <summary>
	/// Gets the Service Desk Contracts API.
	/// </summary>
	public IContracts Contracts { get; private set; }

	/// <summary>
	/// Gets the Service Desk Purchase Orders API.
	/// </summary>
	public IPurchaseOrders PurchaseOrders { get; private set; }

	/// <summary>
	/// Gets the Service Desk Vendors API.
	/// </summary>
	public IVendors Vendors { get; private set; }

	/// <summary>
	/// Gets the Service Desk Tasks API.
	/// </summary>
	public ITasks Tasks { get; private set; }

	/// <summary>
	/// Gets the Service Desk Comments API.
	/// </summary>
	public IComments Comments { get; private set; }

	/// <summary>
	/// Gets the Service Desk Time Tracks API.
	/// </summary>
	public ITimeTracks TimeTracks { get; private set; }

	/// <summary>
	/// Gets the Service Desk Purchases API.
	/// </summary>
	public IPurchases Purchases { get; private set; }

	/// <summary>
	/// Gets the Service Desk Audits API.
	/// </summary>
	public IAudits Audits { get; private set; }

	/// <summary>
	/// Gets the Service Desk Risks API.
	/// </summary>
	public IRisks Risks { get; private set; }

	/// <summary>
	/// Gets the Service Desk Attachments API.
	/// </summary>
	public IAttachments Attachments { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="SolarWindsServiceDeskClient"/> class.
	/// </summary>
	/// <param name="options">The client options.</param>
	public SolarWindsServiceDeskClient(SolarWindsServiceDeskClientOptions options)
	{
		if (options == null)
		{
			throw new ArgumentNullException(nameof(options));
		}

		if (string.IsNullOrWhiteSpace(options.BaseUrl))
		{
			throw new ArgumentException("BaseUrl must be provided.", nameof(options.BaseUrl));
		}

		if (string.IsNullOrWhiteSpace(options.AccessToken))
		{
			throw new ArgumentException("AccessToken must be provided.", nameof(options.AccessToken));
		}

		HttpMessageHandler handler = new HttpClientHandler();
		handler = options.Logger is { } logger
			? new LoggingDelegatingHandler(logger) { InnerHandler = handler }
			: handler;
		handler = new SolarWindsServiceDeskBackingOffHandler(options) { InnerHandler = handler };

		_httpClient = new HttpClient(handler)
		{
			BaseAddress = new Uri(options.BaseUrl)
		};
		_httpClient.Timeout = options.HttpClientTimeout;

		_httpClient.DefaultRequestHeaders.Add("X-Samanage-Authorization", "Bearer " + options.AccessToken);
		_httpClient.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.samanage.v2.1+json");

		Tickets = RestService.For<ITickets>(_httpClient, _refitSettings);
		Users = RestService.For<IUsers>(_httpClient, _refitSettings);
		Incidents = RestService.For<IIncidents>(_httpClient, _refitSettings);
		OtherAssets = RestService.For<IOtherAssets>(_httpClient, _refitSettings);
		ConfigurationItems = RestService.For<IConfigurationItems>(_httpClient, _refitSettings);
		Problems = RestService.For<IProblems>(_httpClient, _refitSettings);
		Changes = RestService.For<IChanges>(_httpClient, _refitSettings);
		Releases = RestService.For<IReleases>(_httpClient, _refitSettings);
		Solutions = RestService.For<ISolutions>(_httpClient, _refitSettings);
		CatalogItems = RestService.For<ICatalogItems>(_httpClient, _refitSettings);
		ServiceRequests = RestService.For<IServiceRequests>(_httpClient, _refitSettings);
		Sites = RestService.For<ISites>(_httpClient, _refitSettings);
		Departments = RestService.For<IDepartments>(_httpClient, _refitSettings);
		Roles = RestService.For<IRoles>(_httpClient, _refitSettings);
		Groups = RestService.For<IGroups>(_httpClient, _refitSettings);
		Categories = RestService.For<ICategories>(_httpClient, _refitSettings);
		Hardwares = RestService.For<IHardwares>(_httpClient, _refitSettings);
		MobileDevices = RestService.For<IMobileDevices>(_httpClient, _refitSettings);
		Softwares = RestService.For<ISoftwares>(_httpClient, _refitSettings);
		Printers = RestService.For<IPrinters>(_httpClient, _refitSettings);
		Contracts = RestService.For<IContracts>(_httpClient, _refitSettings);
		PurchaseOrders = RestService.For<IPurchaseOrders>(_httpClient, _refitSettings);
		Vendors = RestService.For<IVendors>(_httpClient, _refitSettings);
		Tasks = RestService.For<ITasks>(_httpClient, _refitSettings);
		Comments = RestService.For<IComments>(_httpClient, _refitSettings);
		TimeTracks = RestService.For<ITimeTracks>(_httpClient, _refitSettings);
		Purchases = RestService.For<IPurchases>(_httpClient, _refitSettings);
		Audits = RestService.For<IAudits>(_httpClient, _refitSettings);
		Risks = RestService.For<IRisks>(_httpClient, _refitSettings);
		Attachments = RestService.For<IAttachments>(_httpClient, _refitSettings);
	}
}
